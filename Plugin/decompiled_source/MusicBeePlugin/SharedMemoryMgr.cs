using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;

namespace MusicBeePlugin
{
	public class SharedMemoryMgr
	{
		private const ushort DEFAULT_MMF_SIZE = 10240;

		private int sizeofLong = Marshal.SizeOf(typeof(long));

		private MemoryMappedFile mmf;

		private ushort mmfId = 1;

		private SortedDictionary<ushort, ushort> inUse;

		private long freeSpace = 10240L;

		private SortedDictionary<ushort, MemoryMappedFile> submmfs;

		private ushort submmfId = 2;

		public SharedMemoryMgr()
		{
			this.inUse = new SortedDictionary<ushort, ushort>();
			this.submmfs = new SortedDictionary<ushort, MemoryMappedFile>();
			while (this.MMFExists("mbipc_mmf_" + this.mmfId.ToString()))
			{
				if (this.mmfId >= 65535)
				{
					throw new Exception("Unable to create MMF.");
				}
				this.mmfId += 1;
			}
			this.mmf = MemoryMappedFile.CreateNew("mbipc_mmf_" + this.mmfId.ToString(), 10240L);
		}

		public IntPtr Alloc(long capacity)
		{
			long num = capacity + (long)this.sizeofLong;
			IntPtr result;
			if (num > this.freeSpace)
			{
				try
				{
					ushort num2 = this.CreateSubMMF(num);
					this.submmfs[num2].CreateViewAccessor().Write(0L, capacity);
					result = new LRUShort(num2, 0).lr;
					return result;
				}
				catch
				{
					result = IntPtr.Zero;
					return result;
				}
			}
			ushort num3 = 0;
			long num4 = (long)((ulong)num3 + (ulong)num);
			foreach (KeyValuePair<ushort, ushort> current in this.inUse)
			{
				if (num4 < (long)((ulong)current.Key))
				{
					this.inUse.Add(num3, (ushort)num);
					this.freeSpace -= num;
					this.mmf.CreateViewAccessor().Write((long)((ulong)num3), capacity);
					result = new LRUShort(this.mmfId, num3).lr;
					return result;
				}
				num3 = (ushort)(current.Key + current.Value);
				num4 = (long)((ulong)num3 + (ulong)num);
			}
			if (num4 < 10240L)
			{
				this.inUse.Add(num3, (ushort)num);
				this.freeSpace -= num;
				this.mmf.CreateViewAccessor().Write((long)((ulong)num3), capacity);
				return new LRUShort(this.mmfId, num3).lr;
			}
			try
			{
				ushort num5 = this.CreateSubMMF(num);
				this.submmfs[num5].CreateViewAccessor().Write(0L, capacity);
				result = new LRUShort(num5, 0).lr;
			}
			catch
			{
				result = IntPtr.Zero;
			}
			return result;
		}

		public void Free(IntPtr lr)
		{
			LRUShort lRUShort = new LRUShort(lr);
			if (this.submmfs.ContainsKey(lRUShort.low))
			{
				this.FreeSubMMF(lRUShort.low);
				return;
			}
			if (lRUShort.low == this.mmfId && this.inUse.ContainsKey(lRUShort.high))
			{
				this.freeSpace += (long)((ulong)this.inUse[lRUShort.high]);
				this.inUse.Remove(lRUShort.high);
			}
		}

		public MemoryMappedViewAccessor GetAccessor(IntPtr lr)
		{
			LRUShort lRUShort = new LRUShort(lr);
			if (this.submmfs.ContainsKey(lRUShort.low))
			{
				return this.submmfs[lRUShort.low].CreateViewAccessor((long)this.sizeofLong, 0L);
			}
			if (lRUShort.low == this.mmfId && this.inUse.ContainsKey(lRUShort.high))
			{
				return this.mmf.CreateViewAccessor((long)((int)lRUShort.high + this.sizeofLong), (long)((int)this.inUse[lRUShort.high] - this.sizeofLong));
			}
			throw new ArgumentException();
		}

		public MemoryMappedViewStream GetStream(IntPtr lr)
		{
			LRUShort lRUShort = new LRUShort(lr);
			if (this.submmfs.ContainsKey(lRUShort.low))
			{
				return this.submmfs[lRUShort.low].CreateViewStream((long)this.sizeofLong, 0L);
			}
			if (lRUShort.low == this.mmfId && this.inUse.ContainsKey(lRUShort.high))
			{
				return this.mmf.CreateViewStream((long)((int)lRUShort.high + this.sizeofLong), (long)((int)this.inUse[lRUShort.high] - this.sizeofLong));
			}
			throw new ArgumentException();
		}

		public BinaryWriter GetWriter(IntPtr lr)
		{
			LRUShort lRUShort = new LRUShort(lr);
			if (this.submmfs.ContainsKey(lRUShort.low))
			{
				return new BinaryWriter(this.submmfs[lRUShort.low].CreateViewStream((long)this.sizeofLong, 0L));
			}
			if (lRUShort.low == this.mmfId && this.inUse.ContainsKey(lRUShort.high))
			{
				return new BinaryWriter(this.mmf.CreateViewStream((long)((int)lRUShort.high + this.sizeofLong), (long)((int)this.inUse[lRUShort.high] - this.sizeofLong)));
			}
			throw new ArgumentException();
		}

		private ushort CreateSubMMF(long capacity)
		{
			int num = 0;
			while (this.submmfId != 0 && this.MMFExists("mbipc_mmf_" + this.submmfId.ToString()))
			{
				if (num > 65535)
				{
					throw new Exception("Unable to create new sub-MMF.");
				}
				this.submmfId += 1;
				num++;
			}
			this.submmfs.Add(this.submmfId, MemoryMappedFile.CreateNew("mbipc_mmf_" + this.submmfId.ToString(), capacity));
			return this.submmfId;
		}

		private void FreeSubMMF(ushort id)
		{
			this.submmfs.Remove(id);
		}

		private bool MMFExists(string mapName)
		{
			bool result;
			try
			{
				MemoryMappedFile.OpenExisting(mapName);
				result = true;
			}
			catch (FileNotFoundException)
			{
				result = false;
			}
			return result;
		}
	}
}
