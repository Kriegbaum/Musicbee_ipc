using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MusicBeePlugin
{
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public class IPCInterface : NativeWindow
	{
		public enum Bool
		{
			False,
			True
		}

		public enum Error
		{
			Error,
			NoError,
			CommandNotRecognized
		}

		public enum Command
		{
			PlayPause = 100,
			Play,
			Pause,
			Stop,
			StopAfterCurrent,
			PreviousTrack,
			NextTrack,
			StartAutoDj,
			EndAutoDj,
			GetPlayState,
			GetPosition,
			SetPosition,
			GetVolume,
			SetVolume,
			GetVolumep,
			SetVolumep,
			GetVolumef,
			SetVolumef,
			GetMute,
			SetMute,
			GetShuffle,
			SetShuffle,
			GetRepeat,
			SetRepeat,
			GetEqualiserEnabled,
			SetEqualiserEnabled,
			GetDspEnabled,
			SetDspEnabled,
			GetScrobbleEnabled,
			SetScrobbleEnabled,
			ShowEqualiser,
			GetAutoDjEnabled,
			GetStopAfterCurrentEnabled,
			SetStopAfterCurrentEnabled,
			GetCrossfade,
			SetCrossfade,
			GetReplayGainMode,
			SetReplayGainMode,
			QueueRandomTracks,
			GetDuration,
			GetFileUrl,
			GetFileProperty,
			GetFileTag,
			GetLyrics,
			GetDownloadedLyrics,
			GetArtwork,
			GetArtworkUrl,
			GetDownloadedArtwork,
			GetDownloadedArtworkUrl,
			GetArtistPicture,
			GetArtistPictureUrls,
			GetArtistPictureThumb,
			IsSoundtrack,
			GetSoundtrackPictureUrls,
			GetCurrentIndex,
			GetNextIndex,
			IsAnyPriorTracks,
			IsAnyFollowingTracks,
			PlayNow,
			QueueNext,
			QueueLast,
			RemoveAt,
			ClearNowPlayingList,
			MoveFiles,
			ShowNowPlayingAssistant,
			GetShowTimeRemaining,
			GetShowRatingTrack,
			GetShowRatingLove,
			GetButtonEnabled,
			Jump,
			Search,
			SearchFirst,
			SearchIndices,
			SearchFirstIndex,
			SearchAndPlayFirst,
			NowPlayingList_GetListFileUrl = 200,
			NowPlayingList_GetFileProperty,
			NowPlayingList_GetFileTag,
			NowPlayingList_QueryFiles,
			NowPlayingList_QueryGetNextFile,
			NowPlayingList_QueryGetAllFiles,
			NowPlayingList_QueryFilesEx,
			NowPlayingList_PlayLibraryShuffled,
			NowPlayingList_GetItemCount,
			Playlist_GetName = 300,
			Playlist_GetType,
			Playlist_IsInList,
			Playlist_QueryPlaylists,
			Playlist_QueryGetNextPlaylist,
			Playlist_QueryFiles,
			Playlist_QueryGetNextFile,
			Playlist_QueryGetAllFiles,
			Playlist_QueryFilesEx,
			Playlist_CreatePlaylist,
			Playlist_DeletePlaylist,
			Playlist_SetFiles,
			Playlist_AppendFiles,
			Playlist_RemoveAt,
			Playlist_MoveFiles,
			Playlist_PlayNow,
			Playlist_GetItemCount,
			Library_GetFileProperty = 400,
			Library_GetFileTag,
			Library_SetFileTag,
			Library_CommitTagsToFile,
			Library_GetLyrics,
			Library_GetArtwork,
			Library_GetArtworkUrl,
			Library_GetArtistPicture,
			Library_GetArtistPictureUrls,
			Library_GetArtistPictureThumb,
			Library_AddFileToLibrary,
			Library_QueryFiles,
			Library_QueryGetNextFile,
			Library_QueryGetAllFiles,
			Library_QueryFilesEx,
			Library_QuerySimilarArtists,
			Library_QueryLookupTable,
			Library_QueryGetLookupTableValue,
			Library_GetItemCount,
			Library_Jump,
			Library_Search,
			Library_SearchFirst,
			Library_SearchIndices,
			Library_SearchFirstIndex,
			Library_SearchAndPlayFirst,
			Setting_GetFieldName = 700,
			Setting_GetDataType,
			Window_GetHandle = 800,
			Window_Close,
			Window_Restore,
			Window_Minimize,
			Window_Maximize,
			Window_Move,
			Window_Resize,
			Window_BringToFront,
			Window_GetPosition,
			Window_GetSize,
			FreeLRESULT = 900,
			MusicBeeVersion = 995,
			PluginVersion,
			Test,
			MessageBox,
			Probe
		}

		public struct COPYDATASTRUCT
		{
			public IntPtr dwData;

			public uint cbData;

			public IntPtr lpData;
		}

		public struct WINDOWPLACEMENT
		{
			public int length;

			public int flags;

			public int showCmd;

			public Point ptMinPosition;

			public Point ptMaxPosition;

			public Rectangle rcNormalPosition;
		}

		public const int WM_USER = 1024;

		public const int WM_COPYDATA = 74;

		public const int WM_CLOSE = 16;

		public const int SW_HIDE = 0;

		public const int SW_SHOWNORMAL = 1;

		public const int SW_NORMAL = 1;

		public const int SW_SHOWMINIMIZED = 2;

		public const int SW_SHOWMAXIMIZED = 3;

		public const int SW_MAXIMIZE = 3;

		public const int SW_SHOWNOACTIVATE = 4;

		public const int SW_SHOW = 5;

		public const int SW_MINIMIZE = 6;

		public const int SW_SHOWMINNOACTIVE = 7;

		public const int SW_SHOWNA = 8;

		public const int SW_RESTORE = 9;

		public const int SWP_NOSIZE = 1;

		public const int SWP_NOMOVE = 2;

		public const int SWP_NOZORDER = 4;

		public const int SWP_NOREDRAW = 8;

		public const int SWP_NOACTIVATE = 16;

		public const int SWP_DRAWFRAME = 32;

		public const int SWP_FRAMECHANGED = 32;

		public const int SWP_SHOWWINDOW = 64;

		public const int SWP_HIDEWINDOW = 128;

		public const int SWP_NOCOPYBITS = 256;

		public const int SWP_NOOWNERZORDER = 512;

		public const int SWP_NOREPOSITION = 512;

		public const int SWP_NOSENDCHANGING = 1024;

		public const int SWP_DEFERERASE = 8192;

		public const int SWP_ASYNCWINDOWPOS = 16384;

		private Plugin.MusicBeeApiInterface mbApi;

		private SharedMemoryMgr smm;

		private int sizeofInt32 = Marshal.SizeOf(typeof(int));

		private int sizeofDouble = Marshal.SizeOf(typeof(double));

		public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

		public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

		public static readonly IntPtr HWND_TOP = new IntPtr(0);

		public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

		private IPCInterface.Error ToError(bool b)
		{
			if (!b)
			{
				return IPCInterface.Error.Error;
			}
			return IPCInterface.Error.NoError;
		}

		private IntPtr ToErrorIntPtr(bool b)
		{
			if (!b)
			{
				return (IntPtr)0L;
			}
			return (IntPtr)1L;
		}

		private IntPtr ToErrorIntPtr(IntPtr lResult)
		{
			if (!(lResult == IntPtr.Zero))
			{
				return (IntPtr)0L;
			}
			return (IntPtr)1L;
		}

		private IntPtr ToIntPtr(bool b)
		{
			if (!b)
			{
				return (IntPtr)0L;
			}
			return (IntPtr)1L;
		}

		private bool ToBool(IntPtr i)
		{
			return !(i == (IntPtr)0L);
		}

		private bool ToBool(int i)
		{
			return i != 0;
		}

		private string GenQuery(string[] fields, string comparison, string query)
		{
			XElement xElement = new XElement("Conditions", new XAttribute("CombineMethod", "Any"));
			for (int i = 0; i < fields.Length; i++)
			{
				string value = fields[i];
				XElement content = new XElement("Condition", new object[]
				{
					new XAttribute("Field", value),
					new XAttribute("Comparison", comparison),
					new XAttribute("Value", query)
				});
				xElement.Add(content);
			}
			return xElement.ToString();
		}

		private bool GetWinPlacement(out IPCInterface.WINDOWPLACEMENT placement)
		{
			placement = default(IPCInterface.WINDOWPLACEMENT);
			placement.length = Marshal.SizeOf(placement);
			return IPCInterface.GetWindowPlacement(this.mbApi.MB_GetWindowHandle(), ref placement);
		}

		private bool GetWinRect(out Rectangle rect)
		{
			rect = default(Rectangle);
			return IPCInterface.GetWindowRect(this.mbApi.MB_GetWindowHandle(), out rect);
		}

		private bool RestoreWindow()
		{
			IPCInterface.WINDOWPLACEMENT wINDOWPLACEMENT;
			if (!this.GetWinPlacement(out wINDOWPLACEMENT))
			{
				return false;
			}
			wINDOWPLACEMENT.showCmd = 9;
			return IPCInterface.SetWindowPlacement(this.mbApi.MB_GetWindowHandle(), ref wINDOWPLACEMENT);
		}

		private bool MinimizeWindow()
		{
			IPCInterface.WINDOWPLACEMENT wINDOWPLACEMENT;
			if (!this.GetWinPlacement(out wINDOWPLACEMENT))
			{
				return false;
			}
			wINDOWPLACEMENT.showCmd = 6;
			return IPCInterface.SetWindowPlacement(this.mbApi.MB_GetWindowHandle(), ref wINDOWPLACEMENT);
		}

		private bool MaximizeWindow()
		{
			IPCInterface.WINDOWPLACEMENT wINDOWPLACEMENT;
			if (!this.GetWinPlacement(out wINDOWPLACEMENT))
			{
				return false;
			}
			wINDOWPLACEMENT.showCmd = 3;
			return IPCInterface.SetWindowPlacement(this.mbApi.MB_GetWindowHandle(), ref wINDOWPLACEMENT);
		}

		private bool MoveWindow(int x, int y)
		{
			return IPCInterface.SetWindowPos(this.mbApi.MB_GetWindowHandle(), IntPtr.Zero, x, y, 0, 0, 5);
		}

		private bool ResizeWindow(int w, int h)
		{
			return IPCInterface.SetWindowPos(this.mbApi.MB_GetWindowHandle(), IntPtr.Zero, 0, 0, w, h, 6);
		}

		private bool BringWindowToFront()
		{
			return this.RestoreWindow() && IPCInterface.SetForegroundWindow(this.mbApi.MB_GetWindowHandle());
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out int int32_1, out int int32_2)
		{
			int32_1 = 0;
			int32_2 = 0;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 2)))
			{
				return false;
			}
			bool result;
			try
			{
				int32_1 = Marshal.ReadInt32(cds.lpData);
				int32_2 = Marshal.ReadInt32(cds.lpData, this.sizeofInt32);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1)
		{
			string_1 = "";
			if ((ulong)cds.cbData < (ulong)((long)this.sizeofInt32))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out string string_2)
		{
			string_1 = "";
			string_2 = "";
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 2)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_2 = Encoding.Unicode.GetString(array);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out string string_2, out string string_3)
		{
			string_1 = "";
			string_2 = "";
			string_3 = "";
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 3)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_2 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_2 = Encoding.Unicode.GetString(array);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out int int32_1)
		{
			string_1 = "";
			int32_1 = 0;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 2)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				int32_1 = Marshal.ReadInt32(intPtr);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out bool bool_1)
		{
			string_1 = "";
			bool_1 = false;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 2)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				bool_1 = (Marshal.ReadInt32(intPtr) != 0);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out double double_1)
		{
			string_1 = "";
			double_1 = 0.0;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 + this.sizeofDouble)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				double[] array2 = new double[1];
				Marshal.Copy(intPtr, array2, 0, 1);
				double_1 = array2[0];
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out int int32_1, out int int32_2)
		{
			string_1 = "";
			int32_1 = 0;
			int32_2 = 0;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 3)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				int32_1 = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				int32_2 = Marshal.ReadInt32(intPtr);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out int int32_1, out string string_2)
		{
			string_1 = "";
			int32_1 = 0;
			string_2 = "";
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 3)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				int32_1 = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_2 = Encoding.Unicode.GetString(array);
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out int[] int32s_1, out int int32_1)
		{
			int32s_1 = null;
			int32_1 = 0;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 2)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					int32s_1 = new int[num];
					for (int i = 0; i < num; i++)
					{
						int32s_1[i] = Marshal.ReadInt32(intPtr);
						intPtr += this.sizeofInt32;
					}
				}
				int32_1 = Marshal.ReadInt32(intPtr);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out int[] int32s_1, out int int32_1)
		{
			string_1 = "";
			int32s_1 = null;
			int32_1 = 0;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 3)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				int num2 = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num2 > 0)
				{
					int32s_1 = new int[num2];
					for (int i = 0; i < num2; i++)
					{
						int32s_1[i] = Marshal.ReadInt32(intPtr);
						intPtr += this.sizeofInt32;
					}
				}
				int32_1 = Marshal.ReadInt32(intPtr);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out string[] strings_1)
		{
			string_1 = "";
			strings_1 = null;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 2)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				int num2 = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num2 > 0)
				{
					strings_1 = new string[num2];
					for (int i = 0; i < num2; i++)
					{
						num = Marshal.ReadInt32(intPtr);
						intPtr += this.sizeofInt32;
						if (num > 0)
						{
							byte[] array = new byte[num];
							Marshal.Copy(intPtr, array, 0, num);
							strings_1[i] = Encoding.Unicode.GetString(array);
							intPtr += num;
						}
						else
						{
							strings_1[i] = "";
						}
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public bool Unpack(IPCInterface.COPYDATASTRUCT cds, out string string_1, out string string_2, out string[] strings_1)
		{
			string_1 = "";
			string_2 = "";
			strings_1 = null;
			if ((ulong)cds.cbData < (ulong)((long)(this.sizeofInt32 * 3)))
			{
				return false;
			}
			bool result;
			try
			{
				IntPtr intPtr = cds.lpData;
				int num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_1 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				num = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num > 0)
				{
					byte[] array = new byte[num];
					Marshal.Copy(intPtr, array, 0, num);
					string_2 = Encoding.Unicode.GetString(array);
					intPtr += num;
				}
				int num2 = Marshal.ReadInt32(intPtr);
				intPtr += this.sizeofInt32;
				if (num2 > 0)
				{
					strings_1 = new string[num2];
					for (int i = 0; i < num2; i++)
					{
						num = Marshal.ReadInt32(intPtr);
						intPtr += this.sizeofInt32;
						if (num > 0)
						{
							byte[] array = new byte[num];
							Marshal.Copy(intPtr, array, 0, num);
							strings_1[i] = Encoding.Unicode.GetString(array);
							intPtr += num;
						}
						else
						{
							strings_1[i] = "";
						}
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public void FreeLRESULT(IntPtr lr)
		{
			this.smm.Free(lr);
		}

		public IntPtr Pack(string s)
		{
			if (s != null && s.Length > 0)
			{
				int byteCount = Encoding.Unicode.GetByteCount(s);
				if (byteCount == 0)
				{
					return IntPtr.Zero;
				}
				try
				{
					IntPtr intPtr = this.smm.Alloc((long)(this.sizeofInt32 + byteCount));
					IntPtr result;
					if (intPtr == IntPtr.Zero)
					{
						result = IntPtr.Zero;
						return result;
					}
					BinaryWriter writer = this.smm.GetWriter(intPtr);
					writer.Write(byteCount);
					byte[] bytes = Encoding.Unicode.GetBytes(s);
					writer.Write(bytes, 0, byteCount);
					result = intPtr;
					return result;
				}
				catch
				{
					IntPtr result = IntPtr.Zero;
					return result;
				}
			}
			return IntPtr.Zero;
		}

		public IntPtr Pack(string[] strings)
		{
			IntPtr result;
			try
			{
				long num = (long)this.sizeofInt32;
				for (int i = 0; i < strings.Length; i++)
				{
					string text = strings[i];
					num += (long)this.sizeofInt32;
					if (text != null)
					{
						num += (long)Encoding.Unicode.GetByteCount(text);
					}
				}
				IntPtr intPtr = this.smm.Alloc(num);
				if (intPtr == IntPtr.Zero)
				{
					result = IntPtr.Zero;
				}
				else
				{
					BinaryWriter writer = this.smm.GetWriter(intPtr);
					writer.Write(strings.Length);
					for (int j = 0; j < strings.Length; j++)
					{
						string text2 = strings[j];
						int num2;
						if (text2 == null || text2.Length == 0)
						{
							num2 = 0;
						}
						else
						{
							num2 = Encoding.Unicode.GetByteCount(text2);
						}
						writer.Write(num2);
						if (num2 > 0)
						{
							byte[] bytes = Encoding.Unicode.GetBytes(text2);
							writer.Write(bytes, 0, num2);
						}
					}
					result = intPtr;
				}
			}
			catch
			{
				result = IntPtr.Zero;
			}
			return result;
		}

		public IntPtr Pack(int int32_1, int int32_2)
		{
			IntPtr result;
			try
			{
				IntPtr intPtr = this.smm.Alloc((long)(this.sizeofInt32 * 2));
				if (intPtr == IntPtr.Zero)
				{
					result = IntPtr.Zero;
				}
				else
				{
					BinaryWriter writer = this.smm.GetWriter(intPtr);
					writer.Write(int32_1);
					writer.Write(int32_2);
					result = intPtr;
				}
			}
			catch
			{
				result = IntPtr.Zero;
			}
			return result;
		}

		public IntPtr Pack(int[] int32s)
		{
			IntPtr result;
			try
			{
				IntPtr intPtr = this.smm.Alloc((long)(this.sizeofInt32 * (int32s.Length + 1)));
				if (intPtr == IntPtr.Zero)
				{
					result = IntPtr.Zero;
				}
				else
				{
					BinaryWriter writer = this.smm.GetWriter(intPtr);
					writer.Write(int32s.Length);
					for (int i = 0; i < int32s.Length; i++)
					{
						int value = int32s[i];
						writer.Write(value);
					}
					result = intPtr;
				}
			}
			catch
			{
				result = IntPtr.Zero;
			}
			return result;
		}

		public IPCInterface(ref Plugin.MusicBeeApiInterface mbApi)
		{
			CreateParams createParams = new CreateParams();
			createParams.Caption = "MusicBee IPC Interface";
			createParams.ClassName = null;
			createParams.X = 0;
			createParams.Y = 0;
			createParams.Width = 0;
			createParams.Height = 0;
			createParams.Style = 0;
			createParams.Parent = IntPtr.Zero;
			try
			{
				this.CreateHandle(createParams);
			}
			catch (OutOfMemoryException ex)
			{
				MessageBox.Show("Out of memory.", "MusicBee IPC Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				throw ex;
			}
			catch (Win32Exception ex2)
			{
				MessageBox.Show("Could not create specific window:\n" + ex2.Message, "MusicBee IPC Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				throw ex2;
			}
			catch (InvalidOperationException ex3)
			{
				MessageBox.Show("Handle is already assigned.", "MusicBee IPC Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				throw ex3;
			}
			this.mbApi = mbApi;
			try
			{
				this.smm = new SharedMemoryMgr();
			}
			catch (Exception ex4)
			{
				MessageBox.Show("Failed to create Memory-Mapped File", "MusicBee IPC Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				throw ex4;
			}
		}

		~IPCInterface()
		{
			this.Close();
		}

		public void Close()
		{
			if (base.Handle != IntPtr.Zero)
			{
				this.DestroyHandle();
			}
		}

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 74)
			{
				IPCInterface.COPYDATASTRUCT cds = default(IPCInterface.COPYDATASTRUCT);
				cds = (IPCInterface.COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(IPCInterface.COPYDATASTRUCT));
				IPCInterface.Command command = (IPCInterface.Command)((int)m.WParam);
				if (command <= IPCInterface.Command.Playlist_GetItemCount)
				{
					switch (command)
					{
					case IPCInterface.Command.PlayNow:
					{
						string sourceFileUrl;
						if (this.Unpack(cds, out sourceFileUrl))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_PlayNow(sourceFileUrl));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.QueueNext:
					{
						string sourceFileUrl2;
						if (this.Unpack(cds, out sourceFileUrl2))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_QueueNext(sourceFileUrl2));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.QueueLast:
					{
						string sourceFileUrl3;
						if (this.Unpack(cds, out sourceFileUrl3))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_QueueLast(sourceFileUrl3));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.RemoveAt:
					case IPCInterface.Command.ClearNowPlayingList:
					case IPCInterface.Command.ShowNowPlayingAssistant:
					case IPCInterface.Command.GetShowTimeRemaining:
					case IPCInterface.Command.GetShowRatingTrack:
					case IPCInterface.Command.GetShowRatingLove:
					case IPCInterface.Command.GetButtonEnabled:
					case IPCInterface.Command.Jump:
						break;
					case IPCInterface.Command.MoveFiles:
					{
						int[] fromIndices;
						int toIndex;
						if (this.Unpack(cds, out fromIndices, out toIndex))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_MoveFiles(fromIndices, toIndex));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.Search:
					{
						string query;
						string comparison;
						string[] fields;
						if (!this.Unpack(cds, out query, out comparison, out fields))
						{
							m.Result = IntPtr.Zero;
							return;
						}
						string[] strings = null;
						if (this.mbApi.NowPlayingList_QueryFilesEx(this.GenQuery(fields, comparison, query), ref strings))
						{
							m.Result = this.Pack(strings);
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.SearchFirst:
					{
						m.Result = IntPtr.Zero;
						string query2;
						string comparison2;
						string[] fields2;
						if (this.Unpack(cds, out query2, out comparison2, out fields2) && this.mbApi.NowPlayingList_QueryFiles(this.GenQuery(fields2, comparison2, query2)))
						{
							m.Result = this.Pack(this.mbApi.NowPlayingList_QueryGetNextFile());
							return;
						}
						return;
					}
					case IPCInterface.Command.SearchIndices:
					{
						m.Result = (IntPtr)(-1);
						string query3;
						string comparison3;
						string[] fields3;
						if (!this.Unpack(cds, out query3, out comparison3, out fields3))
						{
							return;
						}
						string[] filenames = null;
						if (this.mbApi.NowPlayingList_QueryFilesEx(this.GenQuery(fields3, comparison3, query3), ref filenames))
						{
							int[] array = new int[filenames.Length];
							if (filenames.Length > 0)
							{
								string[] array2 = null;
								this.mbApi.NowPlayingList_QueryFilesEx(null, ref array2);
								int i;
								for (i = 0; i < filenames.Length; i++)
								{
									array[i] = Array.FindIndex<string>(array2, (string s) => s.Equals(filenames[i], StringComparison.CurrentCultureIgnoreCase));
								}
							}
							m.Result = this.Pack(array);
							return;
						}
						return;
					}
					case IPCInterface.Command.SearchFirstIndex:
					{
						m.Result = (IntPtr)(-1);
						string query4;
						string comparison4;
						string[] fields4;
						if (!this.Unpack(cds, out query4, out comparison4, out fields4) || !this.mbApi.NowPlayingList_QueryFiles(this.GenQuery(fields4, comparison4, query4)))
						{
							return;
						}
						string filename = this.mbApi.NowPlayingList_QueryGetNextFile();
						if (filename.Length > 0)
						{
							string[] array3 = null;
							this.mbApi.NowPlayingList_QueryFilesEx(null, ref array3);
							m.Result = (IntPtr)Array.FindIndex<string>(array3, (string s) => s.Equals(filename, StringComparison.CurrentCultureIgnoreCase));
							return;
						}
						return;
					}
					case IPCInterface.Command.SearchAndPlayFirst:
					{
						m.Result = (IntPtr)0L;
						string query5;
						string comparison5;
						string[] fields5;
						if (this.Unpack(cds, out query5, out comparison5, out fields5) && this.mbApi.NowPlayingList_QueryFiles(this.GenQuery(fields5, comparison5, query5)))
						{
							this.mbApi.NowPlayingList_PlayNow(this.mbApi.NowPlayingList_QueryGetNextFile());
							m.Result = (IntPtr)1L;
							return;
						}
						return;
					}
					default:
						switch (command)
						{
						case IPCInterface.Command.NowPlayingList_GetFileProperty:
						{
							int index;
							int type;
							if (this.Unpack(cds, out index, out type))
							{
								string s5 = this.mbApi.NowPlayingList_GetFileProperty(index, (Plugin.FilePropertyType)type);
								m.Result = this.Pack(s5);
								return;
							}
							m.Result = IntPtr.Zero;
							return;
						}
						case IPCInterface.Command.NowPlayingList_GetFileTag:
						{
							int index2;
							int field;
							if (this.Unpack(cds, out index2, out field))
							{
								string s2 = this.mbApi.NowPlayingList_GetFileTag(index2, (Plugin.MetaDataType)field);
								m.Result = this.Pack(s2);
								return;
							}
							m.Result = IntPtr.Zero;
							return;
						}
						case IPCInterface.Command.NowPlayingList_QueryFiles:
						{
							string query6;
							if (this.Unpack(cds, out query6))
							{
								m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_QueryFiles(query6));
								return;
							}
							m.Result = (IntPtr)0L;
							return;
						}
						case IPCInterface.Command.NowPlayingList_QueryGetNextFile:
						case IPCInterface.Command.NowPlayingList_QueryGetAllFiles:
							break;
						case IPCInterface.Command.NowPlayingList_QueryFilesEx:
						{
							string query7;
							if (!this.Unpack(cds, out query7))
							{
								m.Result = (IntPtr)0L;
								return;
							}
							string[] strings2 = null;
							if (this.mbApi.NowPlayingList_QueryFilesEx(query7, ref strings2))
							{
								m.Result = this.Pack(strings2);
								return;
							}
							m.Result = IntPtr.Zero;
							return;
						}
						default:
							switch (command)
							{
							case IPCInterface.Command.Playlist_GetName:
							{
								string playlistUrl;
								if (this.Unpack(cds, out playlistUrl))
								{
									m.Result = this.Pack(this.mbApi.Playlist_GetName(playlistUrl));
									return;
								}
								m.Result = IntPtr.Zero;
								return;
							}
							case IPCInterface.Command.Playlist_GetType:
							{
								string playlistUrl2;
								if (this.Unpack(cds, out playlistUrl2))
								{
									m.Result = (IntPtr)((long)this.mbApi.Playlist_GetType(playlistUrl2));
									return;
								}
								m.Result = IntPtr.Zero;
								return;
							}
							case IPCInterface.Command.Playlist_IsInList:
							{
								string playlistUrl3;
								string filename2;
								if (this.Unpack(cds, out playlistUrl3, out filename2))
								{
									m.Result = this.ToIntPtr(this.mbApi.Playlist_IsInList(playlistUrl3, filename2));
									return;
								}
								m.Result = IntPtr.Zero;
								return;
							}
							case IPCInterface.Command.Playlist_QueryFiles:
							{
								string playlistUrl4;
								if (this.Unpack(cds, out playlistUrl4))
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_QueryFiles(playlistUrl4));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							case IPCInterface.Command.Playlist_QueryFilesEx:
							{
								string playlistUrl5;
								if (!this.Unpack(cds, out playlistUrl5))
								{
									m.Result = IntPtr.Zero;
									return;
								}
								string[] strings3 = null;
								if (this.mbApi.Playlist_QueryFilesEx(playlistUrl5, ref strings3))
								{
									m.Result = this.Pack(strings3);
									return;
								}
								m.Result = IntPtr.Zero;
								return;
							}
							case IPCInterface.Command.Playlist_CreatePlaylist:
							{
								string folderName;
								string playlistName;
								string[] filenames4;
								if (this.Unpack(cds, out folderName, out playlistName, out filenames4))
								{
									m.Result = this.Pack(this.mbApi.Playlist_CreatePlaylist(folderName, playlistName, filenames4));
									return;
								}
								m.Result = IntPtr.Zero;
								return;
							}
							case IPCInterface.Command.Playlist_DeletePlaylist:
							{
								string playlistUrl6;
								if (this.Unpack(cds, out playlistUrl6))
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_DeletePlaylist(playlistUrl6));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							case IPCInterface.Command.Playlist_SetFiles:
							{
								string playlistUrl7;
								string[] filenames2;
								if (this.Unpack(cds, out playlistUrl7, out filenames2))
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_SetFiles(playlistUrl7, filenames2));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							case IPCInterface.Command.Playlist_AppendFiles:
							{
								string playlistUrl8;
								string[] filenames3;
								if (this.Unpack(cds, out playlistUrl8, out filenames3))
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_AppendFiles(playlistUrl8, filenames3));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							case IPCInterface.Command.Playlist_RemoveAt:
							{
								string playlistUrl9;
								int index3;
								if (this.Unpack(cds, out playlistUrl9, out index3))
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_RemoveAt(playlistUrl9, index3));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							case IPCInterface.Command.Playlist_MoveFiles:
							{
								string playlistUrl10;
								int[] fromIndices2;
								int toIndex2;
								if (this.Unpack(cds, out playlistUrl10, out fromIndices2, out toIndex2))
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_MoveFiles(playlistUrl10, fromIndices2, toIndex2));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							case IPCInterface.Command.Playlist_PlayNow:
							{
								string playlistUrl11;
								if (this.Unpack(cds, out playlistUrl11))
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_PlayNow(playlistUrl11));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							case IPCInterface.Command.Playlist_GetItemCount:
							{
								string playlistUrl12;
								if (this.Unpack(cds, out playlistUrl12))
								{
									string[] array4 = null;
									this.mbApi.Playlist_QueryFilesEx(playlistUrl12, ref array4);
									m.Result = (IntPtr)array4.Length;
									return;
								}
								m.Result = IntPtr.Zero;
								return;
							}
							}
							break;
						}
						break;
					}
				}
				else
				{
					switch (command)
					{
					case IPCInterface.Command.Library_GetFileProperty:
					{
						string sourceFileUrl4;
						int type2;
						if (this.Unpack(cds, out sourceFileUrl4, out type2))
						{
							string s3 = this.mbApi.Library_GetFileProperty(sourceFileUrl4, (Plugin.FilePropertyType)type2);
							m.Result = this.Pack(s3);
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_GetFileTag:
					{
						string sourceFileUrl5;
						int field2;
						if (this.Unpack(cds, out sourceFileUrl5, out field2))
						{
							string s4 = this.mbApi.Library_GetFileTag(sourceFileUrl5, (Plugin.MetaDataType)field2);
							m.Result = this.Pack(s4);
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_SetFileTag:
					{
						string sourceFileUrl6;
						int field3;
						string value;
						if (this.Unpack(cds, out sourceFileUrl6, out field3, out value))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.Library_SetFileTag(sourceFileUrl6, (Plugin.MetaDataType)field3, value));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.Library_CommitTagsToFile:
					{
						string sourceFileUrl7;
						if (this.Unpack(cds, out sourceFileUrl7))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.Library_CommitTagsToFile(sourceFileUrl7));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.Library_GetLyrics:
					{
						string sourceFileUrl8;
						int type3;
						if (this.Unpack(cds, out sourceFileUrl8, out type3))
						{
							m.Result = this.Pack(this.mbApi.Library_GetLyrics(sourceFileUrl8, (Plugin.LyricsType)type3));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_GetArtwork:
					{
						string sourceFileUrl9;
						int index4;
						if (this.Unpack(cds, out sourceFileUrl9, out index4))
						{
							m.Result = this.Pack(this.mbApi.Library_GetArtwork(sourceFileUrl9, index4));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_GetArtworkUrl:
					{
						string sourceFileUrl10;
						int index5;
						if (this.Unpack(cds, out sourceFileUrl10, out index5))
						{
							m.Result = this.Pack(this.mbApi.Library_GetArtworkUrl(sourceFileUrl10, index5));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_GetArtistPicture:
					{
						string artistName;
						int fadingPercent;
						int fadingColor;
						if (this.Unpack(cds, out artistName, out fadingPercent, out fadingColor))
						{
							m.Result = this.Pack(this.mbApi.Library_GetArtistPicture(artistName, fadingPercent, fadingColor));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_GetArtistPictureUrls:
					{
						string artistName2;
						bool localOnly;
						if (!this.Unpack(cds, out artistName2, out localOnly))
						{
							m.Result = IntPtr.Zero;
							return;
						}
						string[] strings4 = null;
						if (this.mbApi.Library_GetArtistPictureUrls(artistName2, localOnly, ref strings4))
						{
							m.Result = this.Pack(strings4);
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_GetArtistPictureThumb:
					{
						string artistName3;
						if (this.Unpack(cds, out artistName3))
						{
							m.Result = this.Pack(this.mbApi.Library_GetArtistPictureThumb(artistName3));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_AddFileToLibrary:
					{
						string sourceFileUrl11;
						int category;
						if (this.Unpack(cds, out sourceFileUrl11, out category))
						{
							m.Result = this.Pack(this.mbApi.Library_AddFileToLibrary(sourceFileUrl11, (Plugin.LibraryCategory)category));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_QueryFiles:
					{
						string query8;
						if (this.Unpack(cds, out query8))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.Library_QueryFiles(query8));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.Library_QueryGetNextFile:
					case IPCInterface.Command.Library_QueryGetAllFiles:
					case IPCInterface.Command.Library_GetItemCount:
					case IPCInterface.Command.Library_Jump:
						break;
					case IPCInterface.Command.Library_QueryFilesEx:
					{
						string query9;
						if (!this.Unpack(cds, out query9))
						{
							m.Result = IntPtr.Zero;
							return;
						}
						string[] strings5 = null;
						if (this.mbApi.Library_QueryFilesEx(query9, ref strings5))
						{
							m.Result = this.Pack(strings5);
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_QuerySimilarArtists:
					{
						string artistName4;
						double minimumArtistSimilarityRating;
						if (this.Unpack(cds, out artistName4, out minimumArtistSimilarityRating))
						{
							m.Result = this.Pack(this.mbApi.Library_QuerySimilarArtists(artistName4, minimumArtistSimilarityRating));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_QueryLookupTable:
					{
						string keyTags;
						string valueTags;
						string query10;
						if (this.Unpack(cds, out keyTags, out valueTags, out query10))
						{
							m.Result = this.ToErrorIntPtr(this.mbApi.Library_QueryLookupTable(keyTags, valueTags, query10));
							return;
						}
						m.Result = (IntPtr)0L;
						return;
					}
					case IPCInterface.Command.Library_QueryGetLookupTableValue:
					{
						string key;
						if (this.Unpack(cds, out key))
						{
							m.Result = this.Pack(this.mbApi.Library_QueryGetLookupTableValue(key));
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_Search:
					{
						string query11;
						string comparison6;
						string[] fields6;
						if (!this.Unpack(cds, out query11, out comparison6, out fields6))
						{
							m.Result = IntPtr.Zero;
							return;
						}
						string[] strings6 = null;
						if (this.mbApi.Library_QueryFilesEx(this.GenQuery(fields6, comparison6, query11), ref strings6))
						{
							m.Result = this.Pack(strings6);
							return;
						}
						m.Result = IntPtr.Zero;
						return;
					}
					case IPCInterface.Command.Library_SearchFirst:
					{
						m.Result = IntPtr.Zero;
						string query12;
						string comparison7;
						string[] fields7;
						if (this.Unpack(cds, out query12, out comparison7, out fields7) && this.mbApi.Library_QueryFiles(this.GenQuery(fields7, comparison7, query12)))
						{
							m.Result = this.Pack(this.mbApi.Library_QueryGetNextFile());
							return;
						}
						return;
					}
					case IPCInterface.Command.Library_SearchIndices:
					{
						m.Result = (IntPtr)(-1);
						string query13;
						string comparison8;
						string[] fields8;
						if (!this.Unpack(cds, out query13, out comparison8, out fields8))
						{
							return;
						}
						string[] filenames = null;
						if (this.mbApi.Library_QueryFilesEx(this.GenQuery(fields8, comparison8, query13), ref filenames))
						{
							int[] array5 = new int[filenames.Length];
							if (filenames.Length > 0)
							{
								string[] array6 = null;
								this.mbApi.Library_QueryFilesEx(null, ref array6);
								int i;
								for (i = 0; i < filenames.Length; i++)
								{
									array5[i] = Array.FindIndex<string>(array6, (string s) => s.Equals(filenames[i], StringComparison.CurrentCultureIgnoreCase));
								}
							}
							m.Result = this.Pack(array5);
							return;
						}
						return;
					}
					case IPCInterface.Command.Library_SearchFirstIndex:
					{
						m.Result = (IntPtr)(-1);
						string query14;
						string comparison9;
						string[] fields9;
						if (!this.Unpack(cds, out query14, out comparison9, out fields9) || !this.mbApi.Library_QueryFiles(this.GenQuery(fields9, comparison9, query14)))
						{
							return;
						}
						string filename = this.mbApi.Library_QueryGetNextFile();
						if (filename.Length > 0)
						{
							string[] array7 = null;
							this.mbApi.Library_QueryFilesEx(null, ref array7);
							m.Result = (IntPtr)Array.FindIndex<string>(array7, (string s) => s.Equals(filename, StringComparison.CurrentCultureIgnoreCase));
							return;
						}
						return;
					}
					case IPCInterface.Command.Library_SearchAndPlayFirst:
					{
						m.Result = (IntPtr)0L;
						string query15;
						string comparison10;
						string[] fields10;
						if (this.Unpack(cds, out query15, out comparison10, out fields10) && this.mbApi.Library_QueryFiles(this.GenQuery(fields10, comparison10, query15)))
						{
							this.mbApi.NowPlayingList_PlayNow(this.mbApi.Library_QueryGetNextFile());
							m.Result = (IntPtr)1L;
							return;
						}
						return;
					}
					default:
						switch (command)
						{
						case IPCInterface.Command.Window_Move:
						{
							int x;
							int y;
							if (this.Unpack(cds, out x, out y))
							{
								m.Result = this.ToErrorIntPtr(this.MoveWindow(x, y));
								return;
							}
							m.Result = (IntPtr)0L;
							return;
						}
						case IPCInterface.Command.Window_Resize:
						{
							int w;
							int h;
							if (this.Unpack(cds, out w, out h))
							{
								m.Result = this.ToErrorIntPtr(this.ResizeWindow(w, h));
								return;
							}
							m.Result = (IntPtr)0L;
							return;
						}
						default:
							if (command == IPCInterface.Command.MessageBox)
							{
								string text;
								string caption;
								if (this.Unpack(cds, out text, out caption))
								{
									MessageBox.Show(text, caption);
									m.Result = (IntPtr)1L;
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							break;
						}
						break;
					}
				}
				m.Result = (IntPtr)2L;
				return;
			}
			if (msg == 1024)
			{
				IPCInterface.Command command = (IPCInterface.Command)((int)m.WParam);
				if (command <= IPCInterface.Command.Library_Jump)
				{
					if (command <= IPCInterface.Command.Playlist_QueryGetAllFiles)
					{
						switch (command)
						{
						case IPCInterface.Command.PlayPause:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_PlayPause());
							return;
						case IPCInterface.Command.Play:
							if (this.mbApi.Player_GetPlayState() == Plugin.PlayState.Playing)
							{
								m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetPosition(0));
								return;
							}
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_PlayPause());
							return;
						case IPCInterface.Command.Pause:
							if (this.mbApi.Player_GetPlayState() == Plugin.PlayState.Playing)
							{
								m.Result = this.ToErrorIntPtr(this.mbApi.Player_PlayPause());
								return;
							}
							m.Result = (IntPtr)1L;
							return;
						case IPCInterface.Command.Stop:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_Stop());
							return;
						case IPCInterface.Command.StopAfterCurrent:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_StopAfterCurrent());
							return;
						case IPCInterface.Command.PreviousTrack:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_PlayPreviousTrack());
							return;
						case IPCInterface.Command.NextTrack:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_PlayNextTrack());
							return;
						case IPCInterface.Command.StartAutoDj:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_StartAutoDj());
							return;
						case IPCInterface.Command.EndAutoDj:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_EndAutoDj());
							return;
						case IPCInterface.Command.GetPlayState:
							m.Result = (IntPtr)((long)this.mbApi.Player_GetPlayState());
							return;
						case IPCInterface.Command.GetPosition:
							m.Result = (IntPtr)this.mbApi.Player_GetPosition();
							return;
						case IPCInterface.Command.SetPosition:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetPosition((int)m.LParam));
							return;
						case IPCInterface.Command.GetVolume:
							m.Result = (IntPtr)((long)(this.mbApi.Player_GetVolume() * 100f));
							return;
						case IPCInterface.Command.SetVolume:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetVolume((float)((long)m.LParam) / 100f));
							return;
						case IPCInterface.Command.GetVolumep:
							m.Result = (IntPtr)((long)(this.mbApi.Player_GetVolume() * 10000f));
							return;
						case IPCInterface.Command.SetVolumep:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetVolume((float)((long)m.LParam) / 10000f));
							return;
						case IPCInterface.Command.GetVolumef:
							m.Result = (IntPtr)new FloatInt(this.mbApi.Player_GetVolume()).i;
							return;
						case IPCInterface.Command.SetVolumef:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetVolume(new FloatInt((int)m.LParam).f));
							return;
						case IPCInterface.Command.GetMute:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetMute());
							return;
						case IPCInterface.Command.SetMute:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetMute(this.ToBool(m.LParam)));
							return;
						case IPCInterface.Command.GetShuffle:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetShuffle());
							return;
						case IPCInterface.Command.SetShuffle:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetShuffle(this.ToBool(m.LParam)));
							return;
						case IPCInterface.Command.GetRepeat:
							m.Result = (IntPtr)((long)this.mbApi.Player_GetRepeat());
							return;
						case IPCInterface.Command.SetRepeat:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetRepeat((Plugin.RepeatMode)((int)m.LParam)));
							return;
						case IPCInterface.Command.GetEqualiserEnabled:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetEqualiserEnabled());
							return;
						case IPCInterface.Command.SetEqualiserEnabled:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetEqualiserEnabled(this.ToBool(m.LParam)));
							return;
						case IPCInterface.Command.GetDspEnabled:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetDspEnabled());
							return;
						case IPCInterface.Command.SetDspEnabled:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetDspEnabled(this.ToBool(m.LParam)));
							return;
						case IPCInterface.Command.GetScrobbleEnabled:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetScrobbleEnabled());
							return;
						case IPCInterface.Command.SetScrobbleEnabled:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetScrobbleEnabled(this.ToBool(m.LParam)));
							return;
						case IPCInterface.Command.ShowEqualiser:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_ShowEqualiser());
							return;
						case IPCInterface.Command.GetAutoDjEnabled:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetAutoDjEnabled());
							return;
						case IPCInterface.Command.GetStopAfterCurrentEnabled:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetStopAfterCurrentEnabled());
							return;
						case IPCInterface.Command.SetStopAfterCurrentEnabled:
							if (this.mbApi.Player_GetStopAfterCurrentEnabled() != this.ToBool(m.LParam))
							{
								m.Result = this.ToErrorIntPtr(this.mbApi.Player_StopAfterCurrent());
								return;
							}
							m.Result = (IntPtr)1L;
							return;
						case IPCInterface.Command.GetCrossfade:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetCrossfade());
							return;
						case IPCInterface.Command.SetCrossfade:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetCrossfade(this.ToBool(m.LParam)));
							return;
						case IPCInterface.Command.GetReplayGainMode:
							m.Result = (IntPtr)((long)this.mbApi.Player_GetReplayGainMode());
							return;
						case IPCInterface.Command.SetReplayGainMode:
							m.Result = this.ToErrorIntPtr(this.mbApi.Player_SetReplayGainMode((Plugin.ReplayGainMode)((int)m.LParam)));
							return;
						case IPCInterface.Command.QueueRandomTracks:
							m.Result = (IntPtr)this.mbApi.Player_QueueRandomTracks((int)m.LParam);
							return;
						case IPCInterface.Command.GetDuration:
							m.Result = (IntPtr)this.mbApi.NowPlaying_GetDuration();
							return;
						case IPCInterface.Command.GetFileUrl:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetFileUrl());
							return;
						case IPCInterface.Command.GetFileProperty:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetFileProperty((Plugin.FilePropertyType)((int)m.LParam)));
							return;
						case IPCInterface.Command.GetFileTag:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetFileTag((Plugin.MetaDataType)((int)m.LParam)));
							return;
						case IPCInterface.Command.GetLyrics:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetLyrics());
							return;
						case IPCInterface.Command.GetDownloadedLyrics:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetDownloadedLyrics());
							return;
						case IPCInterface.Command.GetArtwork:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetArtwork());
							return;
						case IPCInterface.Command.GetArtworkUrl:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetArtworkUrl());
							return;
						case IPCInterface.Command.GetDownloadedArtwork:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetDownloadedArtwork());
							return;
						case IPCInterface.Command.GetDownloadedArtworkUrl:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetDownloadedArtworkUrl());
							return;
						case IPCInterface.Command.GetArtistPicture:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetArtistPicture((int)m.LParam));
							return;
						case IPCInterface.Command.GetArtistPictureUrls:
						{
							string[] strings7 = null;
							if (this.mbApi.NowPlaying_GetArtistPictureUrls(this.ToBool(m.LParam), ref strings7))
							{
								m.Result = this.Pack(strings7);
								return;
							}
							m.Result = IntPtr.Zero;
							return;
						}
						case IPCInterface.Command.GetArtistPictureThumb:
							m.Result = this.Pack(this.mbApi.NowPlaying_GetArtistPictureThumb());
							return;
						case IPCInterface.Command.IsSoundtrack:
							m.Result = this.ToIntPtr(this.mbApi.NowPlaying_IsSoundtrack());
							return;
						case IPCInterface.Command.GetSoundtrackPictureUrls:
						{
							string[] strings8 = null;
							if (this.mbApi.NowPlaying_GetSoundtrackPictureUrls(this.ToBool(m.LParam), ref strings8))
							{
								m.Result = this.Pack(strings8);
								return;
							}
							m.Result = IntPtr.Zero;
							return;
						}
						case IPCInterface.Command.GetCurrentIndex:
							m.Result = (IntPtr)this.mbApi.NowPlayingList_GetCurrentIndex();
							return;
						case IPCInterface.Command.GetNextIndex:
							m.Result = (IntPtr)this.mbApi.NowPlayingList_GetNextIndex((int)m.LParam);
							return;
						case IPCInterface.Command.IsAnyPriorTracks:
							m.Result = this.ToIntPtr(this.mbApi.NowPlayingList_IsAnyPriorTracks());
							return;
						case IPCInterface.Command.IsAnyFollowingTracks:
							m.Result = this.ToIntPtr(this.mbApi.NowPlayingList_IsAnyFollowingTracks());
							return;
						case IPCInterface.Command.PlayNow:
						case IPCInterface.Command.QueueNext:
						case IPCInterface.Command.QueueLast:
						case IPCInterface.Command.MoveFiles:
						case IPCInterface.Command.Search:
						case IPCInterface.Command.SearchFirst:
						case IPCInterface.Command.SearchIndices:
						case IPCInterface.Command.SearchFirstIndex:
						case IPCInterface.Command.SearchAndPlayFirst:
						case (IPCInterface.Command)175:
						case (IPCInterface.Command)176:
						case (IPCInterface.Command)177:
						case (IPCInterface.Command)178:
						case (IPCInterface.Command)179:
						case (IPCInterface.Command)180:
						case (IPCInterface.Command)181:
						case (IPCInterface.Command)182:
						case (IPCInterface.Command)183:
						case (IPCInterface.Command)184:
						case (IPCInterface.Command)185:
						case (IPCInterface.Command)186:
						case (IPCInterface.Command)187:
						case (IPCInterface.Command)188:
						case (IPCInterface.Command)189:
						case (IPCInterface.Command)190:
						case (IPCInterface.Command)191:
						case (IPCInterface.Command)192:
						case (IPCInterface.Command)193:
						case (IPCInterface.Command)194:
						case (IPCInterface.Command)195:
						case (IPCInterface.Command)196:
						case (IPCInterface.Command)197:
						case (IPCInterface.Command)198:
						case (IPCInterface.Command)199:
						case IPCInterface.Command.NowPlayingList_GetFileProperty:
						case IPCInterface.Command.NowPlayingList_GetFileTag:
						case IPCInterface.Command.NowPlayingList_QueryFiles:
						case IPCInterface.Command.NowPlayingList_QueryFilesEx:
							break;
						case IPCInterface.Command.RemoveAt:
							m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_RemoveAt((int)m.LParam));
							return;
						case IPCInterface.Command.ClearNowPlayingList:
							m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_Clear());
							return;
						case IPCInterface.Command.ShowNowPlayingAssistant:
							m.Result = this.ToErrorIntPtr(this.mbApi.MB_ShowNowPlayingAssistant());
							return;
						case IPCInterface.Command.GetShowTimeRemaining:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetShowTimeRemaining());
							return;
						case IPCInterface.Command.GetShowRatingTrack:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetShowRatingTrack());
							return;
						case IPCInterface.Command.GetShowRatingLove:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetShowRatingLove());
							return;
						case IPCInterface.Command.GetButtonEnabled:
							m.Result = this.ToIntPtr(this.mbApi.Player_GetButtonEnabled((Plugin.PlayButtonType)((int)m.LParam)));
							return;
						case IPCInterface.Command.Jump:
						{
							int num = (int)m.LParam;
							string[] array8 = null;
							this.mbApi.NowPlayingList_QueryFilesEx(null, ref array8);
							if (num < array8.Length)
							{
								m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_PlayNow(array8[num]));
								return;
							}
							m.Result = (IntPtr)0L;
							return;
						}
						case IPCInterface.Command.NowPlayingList_GetListFileUrl:
							m.Result = this.Pack(this.mbApi.NowPlayingList_GetListFileUrl((int)m.LParam));
							return;
						case IPCInterface.Command.NowPlayingList_QueryGetNextFile:
							m.Result = this.Pack(this.mbApi.NowPlayingList_QueryGetNextFile());
							return;
						case IPCInterface.Command.NowPlayingList_QueryGetAllFiles:
							m.Result = this.Pack(this.mbApi.NowPlayingList_QueryGetAllFiles());
							return;
						case IPCInterface.Command.NowPlayingList_PlayLibraryShuffled:
							m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_PlayLibraryShuffled());
							return;
						case IPCInterface.Command.NowPlayingList_GetItemCount:
						{
							string[] array9 = null;
							this.mbApi.NowPlayingList_QueryFilesEx(null, ref array9);
							m.Result = (IntPtr)array9.Length;
							return;
						}
						default:
							switch (command)
							{
							case IPCInterface.Command.Playlist_QueryPlaylists:
								m.Result = this.ToErrorIntPtr(this.mbApi.Playlist_QueryPlaylists());
								return;
							case IPCInterface.Command.Playlist_QueryGetNextPlaylist:
								m.Result = this.Pack(this.mbApi.Playlist_QueryGetNextPlaylist());
								return;
							case IPCInterface.Command.Playlist_QueryGetNextFile:
								m.Result = this.Pack(this.mbApi.Playlist_QueryGetNextFile());
								return;
							case IPCInterface.Command.Playlist_QueryGetAllFiles:
								m.Result = this.Pack(this.mbApi.Playlist_QueryGetAllFiles());
								return;
							}
							break;
						}
					}
					else
					{
						switch (command)
						{
						case IPCInterface.Command.Library_QueryGetNextFile:
							m.Result = this.Pack(this.mbApi.Library_QueryGetNextFile());
							return;
						case IPCInterface.Command.Library_QueryGetAllFiles:
							m.Result = this.Pack(this.mbApi.Library_QueryGetAllFiles());
							return;
						default:
							switch (command)
							{
							case IPCInterface.Command.Library_GetItemCount:
							{
								string[] array10 = null;
								this.mbApi.Library_QueryFilesEx(null, ref array10);
								m.Result = (IntPtr)array10.Length;
								return;
							}
							case IPCInterface.Command.Library_Jump:
							{
								int num2 = (int)m.LParam;
								string[] array11 = null;
								this.mbApi.Library_QueryFilesEx(null, ref array11);
								if (num2 < array11.Length)
								{
									m.Result = this.ToErrorIntPtr(this.mbApi.NowPlayingList_PlayNow(array11[num2]));
									return;
								}
								m.Result = (IntPtr)0L;
								return;
							}
							}
							break;
						}
					}
				}
				else if (command <= IPCInterface.Command.Window_GetSize)
				{
					switch (command)
					{
					case IPCInterface.Command.Setting_GetFieldName:
						m.Result = this.Pack(this.mbApi.Setting_GetFieldName((Plugin.MetaDataType)((int)m.LParam)));
						return;
					case IPCInterface.Command.Setting_GetDataType:
						m.Result = (IntPtr)((long)this.mbApi.Setting_GetDataType((Plugin.MetaDataType)((int)m.LParam)));
						return;
					default:
						switch (command)
						{
						case IPCInterface.Command.Window_GetHandle:
							m.Result = this.mbApi.MB_GetWindowHandle();
							return;
						case IPCInterface.Command.Window_Close:
							m.Result = this.ToErrorIntPtr(IPCInterface.SendMessage(this.mbApi.MB_GetWindowHandle(), 16u, UIntPtr.Zero, IntPtr.Zero));
							return;
						case IPCInterface.Command.Window_Restore:
							m.Result = this.ToErrorIntPtr(this.RestoreWindow());
							return;
						case IPCInterface.Command.Window_Minimize:
							m.Result = this.ToErrorIntPtr(this.MinimizeWindow());
							return;
						case IPCInterface.Command.Window_Maximize:
							m.Result = this.ToErrorIntPtr(this.MaximizeWindow());
							return;
						case IPCInterface.Command.Window_BringToFront:
							m.Result = this.ToErrorIntPtr(this.BringWindowToFront());
							return;
						case IPCInterface.Command.Window_GetPosition:
						{
							Rectangle rectangle;
							if (this.GetWinRect(out rectangle))
							{
								m.Result = this.Pack(rectangle.X, rectangle.Y);
								return;
							}
							m.Result = IntPtr.Zero;
							return;
						}
						case IPCInterface.Command.Window_GetSize:
						{
							Rectangle rectangle2;
							if (this.GetWinRect(out rectangle2))
							{
								m.Result = this.Pack(rectangle2.Width, rectangle2.Height);
								return;
							}
							m.Result = IntPtr.Zero;
							return;
						}
						}
						break;
					}
				}
				else
				{
					if (command == IPCInterface.Command.FreeLRESULT)
					{
						this.FreeLRESULT(m.LParam);
						m.Result = (IntPtr)1L;
						return;
					}
					switch (command)
					{
					case IPCInterface.Command.MusicBeeVersion:
						m.Result = (IntPtr)((long)this.mbApi.MusicBeeVersion);
						return;
					case IPCInterface.Command.PluginVersion:
						m.Result = this.Pack(typeof(IPCInterface).Assembly.GetName().Version.ToString());
						return;
					case IPCInterface.Command.Test:
						MessageBox.Show("Test", "MusicBee IPC");
						m.Result = (IntPtr)1L;
						return;
					case IPCInterface.Command.Probe:
						m.Result = (IntPtr)1L;
						return;
					}
				}
				m.Result = (IntPtr)2L;
				return;
			}
			base.DefWndProc(ref m);
		}

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, UIntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowPlacement(IntPtr hWnd, ref IPCInterface.WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPlacement(IntPtr hWnd, ref IPCInterface.WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
	}
}
