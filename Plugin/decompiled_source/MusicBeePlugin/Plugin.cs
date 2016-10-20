using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace MusicBeePlugin
{
	public class Plugin
	{
		public struct MusicBeeApiInterface
		{
			public short InterfaceVersion;

			public short ApiRevision;

			public Plugin.MB_ReleaseStringDelegate MB_ReleaseString;

			public Plugin.MB_TraceDelegate MB_Trace;

			public Plugin.Setting_GetPersistentStoragePathDelegate Setting_GetPersistentStoragePath;

			public Plugin.Setting_GetSkinDelegate Setting_GetSkin;

			public Plugin.Setting_GetSkinElementColourDelegate Setting_GetSkinElementColour;

			public Plugin.Setting_IsWindowBordersSkinnedDelegate Setting_IsWindowBordersSkinned;

			public Plugin.Library_GetFilePropertyDelegate Library_GetFileProperty;

			public Plugin.Library_GetFileTagDelegate Library_GetFileTag;

			public Plugin.Library_SetFileTagDelegate Library_SetFileTag;

			public Plugin.Library_CommitTagsToFileDelegate Library_CommitTagsToFile;

			public Plugin.Library_GetLyricsDelegate Library_GetLyrics;

			public Plugin.Library_GetArtworkDelegate Library_GetArtwork;

			public Plugin.Library_QueryFilesDelegate Library_QueryFiles;

			public Plugin.Library_QueryGetNextFileDelegate Library_QueryGetNextFile;

			public Plugin.Player_GetPositionDelegate Player_GetPosition;

			public Plugin.Player_SetPositionDelegate Player_SetPosition;

			public Plugin.Player_GetPlayStateDelegate Player_GetPlayState;

			public Plugin.Player_ActionDelegate Player_PlayPause;

			public Plugin.Player_ActionDelegate Player_Stop;

			public Plugin.Player_ActionDelegate Player_StopAfterCurrent;

			public Plugin.Player_ActionDelegate Player_PlayPreviousTrack;

			public Plugin.Player_ActionDelegate Player_PlayNextTrack;

			public Plugin.Player_ActionDelegate Player_StartAutoDj;

			public Plugin.Player_ActionDelegate Player_EndAutoDj;

			public Plugin.Player_GetVolumeDelegate Player_GetVolume;

			public Plugin.Player_SetVolumeDelegate Player_SetVolume;

			public Plugin.Player_GetMuteDelegate Player_GetMute;

			public Plugin.Player_SetMuteDelegate Player_SetMute;

			public Plugin.Player_GetShuffleDelegate Player_GetShuffle;

			public Plugin.Player_SetShuffleDelegate Player_SetShuffle;

			public Plugin.Player_GetRepeatDelegate Player_GetRepeat;

			public Plugin.Player_SetRepeatDelegate Player_SetRepeat;

			public Plugin.Player_GetEqualiserEnabledDelegate Player_GetEqualiserEnabled;

			public Plugin.Player_SetEqualiserEnabledDelegate Player_SetEqualiserEnabled;

			public Plugin.Player_GetDspEnabledDelegate Player_GetDspEnabled;

			public Plugin.Player_SetDspEnabledDelegate Player_SetDspEnabled;

			public Plugin.Player_GetScrobbleEnabledDelegate Player_GetScrobbleEnabled;

			public Plugin.Player_SetScrobbleEnabledDelegate Player_SetScrobbleEnabled;

			public Plugin.NowPlaying_GetFileUrlDelegate NowPlaying_GetFileUrl;

			public Plugin.NowPlaying_GetDurationDelegate NowPlaying_GetDuration;

			public Plugin.NowPlaying_GetFilePropertyDelegate NowPlaying_GetFileProperty;

			public Plugin.NowPlaying_GetFileTagDelegate NowPlaying_GetFileTag;

			public Plugin.NowPlaying_GetLyricsDelegate NowPlaying_GetLyrics;

			public Plugin.NowPlaying_GetArtworkDelegate NowPlaying_GetArtwork;

			public Plugin.NowPlayingList_ActionDelegate NowPlayingList_Clear;

			public Plugin.Library_QueryFilesDelegate NowPlayingList_QueryFiles;

			public Plugin.Library_QueryGetNextFileDelegate NowPlayingList_QueryGetNextFile;

			public Plugin.NowPlayingList_FileActionDelegate NowPlayingList_PlayNow;

			public Plugin.NowPlayingList_FileActionDelegate NowPlayingList_QueueNext;

			public Plugin.NowPlayingList_FileActionDelegate NowPlayingList_QueueLast;

			public Plugin.NowPlayingList_ActionDelegate NowPlayingList_PlayLibraryShuffled;

			public Plugin.Playlist_QueryPlaylistsDelegate Playlist_QueryPlaylists;

			public Plugin.Playlist_QueryGetNextPlaylistDelegate Playlist_QueryGetNextPlaylist;

			public Plugin.Playlist_GetTypeDelegate Playlist_GetType;

			public Plugin.Playlist_QueryFilesDelegate Playlist_QueryFiles;

			public Plugin.Library_QueryGetNextFileDelegate Playlist_QueryGetNextFile;

			public Plugin.MB_WindowHandleDelegate MB_GetWindowHandle;

			public Plugin.MB_RefreshPanelsDelegate MB_RefreshPanels;

			public Plugin.MB_SendNotificationDelegate MB_SendNotification;

			public Plugin.MB_AddMenuItemDelegate MB_AddMenuItem;

			public Plugin.Setting_GetFieldNameDelegate Setting_GetFieldName;

			public Plugin.Library_QueryGetAllFilesDelegate Library_QueryGetAllFiles;

			public Plugin.Library_QueryGetAllFilesDelegate NowPlayingList_QueryGetAllFiles;

			public Plugin.Library_QueryGetAllFilesDelegate Playlist_QueryGetAllFiles;

			public Plugin.MB_CreateBackgroundTaskDelegate MB_CreateBackgroundTask;

			public Plugin.MB_SetBackgroundTaskMessageDelegate MB_SetBackgroundTaskMessage;

			public Plugin.MB_RegisterCommandDelegate MB_RegisterCommand;

			public Plugin.Setting_GetDefaultFontDelegate Setting_GetDefaultFont;

			public Plugin.Player_GetShowTimeRemainingDelegate Player_GetShowTimeRemaining;

			public Plugin.NowPlayingList_GetCurrentIndexDelegate NowPlayingList_GetCurrentIndex;

			public Plugin.NowPlayingList_GetFileUrlDelegate NowPlayingList_GetListFileUrl;

			public Plugin.NowPlayingList_GetFilePropertyDelegate NowPlayingList_GetFileProperty;

			public Plugin.NowPlayingList_GetFileTagDelegate NowPlayingList_GetFileTag;

			public Plugin.NowPlaying_GetSpectrumDataDelegate NowPlaying_GetSpectrumData;

			public Plugin.NowPlaying_GetSoundGraphDelegate NowPlaying_GetSoundGraph;

			public Plugin.MB_GetPanelBoundsDelegate MB_GetPanelBounds;

			public Plugin.MB_AddPanelDelegate MB_AddPanel;

			public Plugin.MB_RemovePanelDelegate MB_RemovePanel;

			public Plugin.MB_GetLocalisationDelegate MB_GetLocalisation;

			public Plugin.NowPlayingList_IsAnyPriorTracksDelegate NowPlayingList_IsAnyPriorTracks;

			public Plugin.NowPlayingList_IsAnyFollowingTracksDelegate NowPlayingList_IsAnyFollowingTracks;

			public Plugin.Player_ShowEqualiserDelegate Player_ShowEqualiser;

			public Plugin.Player_GetAutoDjEnabledDelegate Player_GetAutoDjEnabled;

			public Plugin.Player_GetStopAfterCurrentEnabledDelegate Player_GetStopAfterCurrentEnabled;

			public Plugin.Player_GetCrossfadeDelegate Player_GetCrossfade;

			public Plugin.Player_SetCrossfadeDelegate Player_SetCrossfade;

			public Plugin.Player_GetReplayGainModeDelegate Player_GetReplayGainMode;

			public Plugin.Player_SetReplayGainModeDelegate Player_SetReplayGainMode;

			public Plugin.Player_QueueRandomTracksDelegate Player_QueueRandomTracks;

			public Plugin.Setting_GetDataTypeDelegate Setting_GetDataType;

			public Plugin.NowPlayingList_GetNextIndexDelegate NowPlayingList_GetNextIndex;

			public Plugin.NowPlaying_GetArtistPictureDelegate NowPlaying_GetArtistPicture;

			public Plugin.NowPlaying_GetArtworkDelegate NowPlaying_GetDownloadedArtwork;

			public Plugin.MB_ShowNowPlayingAssistantDelegate MB_ShowNowPlayingAssistant;

			public Plugin.NowPlaying_GetLyricsDelegate NowPlaying_GetDownloadedLyrics;

			public Plugin.Player_GetShowRatingTrackDelegate Player_GetShowRatingTrack;

			public Plugin.Player_GetShowRatingLoveDelegate Player_GetShowRatingLove;

			public Plugin.MB_CreateParameterisedBackgroundTaskDelegate MB_CreateParameterisedBackgroundTask;

			public Plugin.Setting_GetLastFmUserIdDelegate Setting_GetLastFmUserId;

			public Plugin.Playlist_GetNameDelegate Playlist_GetName;

			public Plugin.Playlist_CreatePlaylistDelegate Playlist_CreatePlaylist;

			public Plugin.Playlist_SetFilesDelegate Playlist_SetFiles;

			public Plugin.Library_QuerySimilarArtistsDelegate Library_QuerySimilarArtists;

			public Plugin.Library_QueryLookupTableDelegate Library_QueryLookupTable;

			public Plugin.Library_QueryGetLookupTableValueDelegate Library_QueryGetLookupTableValue;

			public Plugin.NowPlayingList_FilesActionDelegate NowPlayingList_QueueFilesNext;

			public Plugin.NowPlayingList_FilesActionDelegate NowPlayingList_QueueFilesLast;

			public Plugin.Setting_GetWebProxyDelegate Setting_GetWebProxy;

			public Plugin.NowPlayingList_RemoveAtDelegate NowPlayingList_RemoveAt;

			public Plugin.Playlist_RemoveAtDelegate Playlist_RemoveAt;

			public Plugin.MB_SetPanelScrollableAreaDelegate MB_SetPanelScrollableArea;

			public Plugin.MB_InvokeCommandDelegate MB_InvokeCommand;

			public Plugin.MB_OpenFilterInTabDelegate MB_OpenFilterInTab;

			public Plugin.MB_SetWindowSizeDelegate MB_SetWindowSize;

			public Plugin.Library_GetArtistPictureDelegate Library_GetArtistPicture;

			public Plugin.Pending_GetFileUrlDelegate Pending_GetFileUrl;

			public Plugin.Pending_GetFilePropertyDelegate Pending_GetFileProperty;

			public Plugin.Pending_GetFileTagDelegate Pending_GetFileTag;

			public Plugin.Player_GetButtonEnabledDelegate Player_GetButtonEnabled;

			public Plugin.NowPlayingList_MoveFilesDelegate NowPlayingList_MoveFiles;

			public Plugin.Library_GetArtworkDelegate Library_GetArtworkUrl;

			public Plugin.Library_GetArtistPictureThumbDelegate Library_GetArtistPictureThumb;

			public Plugin.NowPlaying_GetArtworkDelegate NowPlaying_GetArtworkUrl;

			public Plugin.NowPlaying_GetArtworkDelegate NowPlaying_GetDownloadedArtworkUrl;

			public Plugin.NowPlaying_GetArtistPictureThumbDelegate NowPlaying_GetArtistPictureThumb;

			public Plugin.Playlist_IsInListDelegate Playlist_IsInList;

			public Plugin.Library_GetArtistPictureUrlsDelegate Library_GetArtistPictureUrls;

			public Plugin.NowPlaying_GetArtistPictureUrlsDelegate NowPlaying_GetArtistPictureUrls;

			public Plugin.Playlist_AddFilesDelegate Playlist_AppendFiles;

			public Plugin.Sync_FileStartDelegate Sync_FileStart;

			public Plugin.Sync_FileEndDelegate Sync_FileEnd;

			public Plugin.Library_QueryFilesExDelegate Library_QueryFilesEx;

			public Plugin.Library_QueryFilesExDelegate NowPlayingList_QueryFilesEx;

			public Plugin.Playlist_QueryFilesExDelegate Playlist_QueryFilesEx;

			public Plugin.Playlist_MoveFilesDelegate Playlist_MoveFiles;

			public Plugin.Playlist_PlayNowDelegate Playlist_PlayNow;

			public Plugin.NowPlaying_IsSoundtrackDelegate NowPlaying_IsSoundtrack;

			public Plugin.NowPlaying_GetArtistPictureUrlsDelegate NowPlaying_GetSoundtrackPictureUrls;

			public Plugin.Library_GetDevicePersistentIdDelegate Library_GetDevicePersistentId;

			public Plugin.Library_SetDevicePersistentIdDelegate Library_SetDevicePersistentId;

			public Plugin.Library_FindDevicePersistentIdDelegate Library_FindDevicePersistentId;

			public Plugin.Setting_GetValueDelegate Setting_GetValue;

			public Plugin.Library_AddFileToLibraryDelegate Library_AddFileToLibrary;

			public Plugin.Playlist_DeletePlaylistDelegate Playlist_DeletePlaylist;

			public Plugin.Library_GetSyncDeltaDelegate Library_GetSyncDelta;

			public Plugin.MusicBeeVersion MusicBeeVersion
			{
				get
				{
					if (this.ApiRevision <= 25)
					{
						return Plugin.MusicBeeVersion.v2_0;
					}
					if (this.ApiRevision <= 31)
					{
						return Plugin.MusicBeeVersion.v2_1;
					}
					if (this.ApiRevision <= 33)
					{
						return Plugin.MusicBeeVersion.v2_2;
					}
					return Plugin.MusicBeeVersion.v2_3;
				}
			}

			public void Initialise(IntPtr apiInterfacePtr)
			{
				Plugin.CopyMemory(ref this, apiInterfacePtr, 4);
				if (this.MusicBeeVersion == Plugin.MusicBeeVersion.v2_0)
				{
					Plugin.CopyMemory(ref this, apiInterfacePtr, 456);
					return;
				}
				if (this.MusicBeeVersion == Plugin.MusicBeeVersion.v2_1)
				{
					Plugin.CopyMemory(ref this, apiInterfacePtr, 516);
					return;
				}
				if (this.MusicBeeVersion == Plugin.MusicBeeVersion.v2_2)
				{
					Plugin.CopyMemory(ref this, apiInterfacePtr, 584);
					return;
				}
				Plugin.CopyMemory(ref this, apiInterfacePtr, Marshal.SizeOf(this));
			}
		}

		public enum MusicBeeVersion
		{
			v2_0,
			v2_1,
			v2_2,
			v2_3
		}

		public enum PluginType
		{
			Unknown,
			General,
			LyricsRetrieval,
			ArtworkRetrieval,
			PanelView,
			DataStream,
			InstantMessenger,
			Storage
		}

		[StructLayout(LayoutKind.Sequential)]
		public class PluginInfo
		{
			public short PluginInfoVersion;

			public Plugin.PluginType Type;

			public string Name;

			public string Description;

			public string Author;

			public string TargetApplication;

			public short VersionMajor;

			public short VersionMinor;

			public short Revision;

			public short MinInterfaceVersion;

			public short MinApiRevision;

			public Plugin.ReceiveNotificationFlags ReceiveNotifications;

			public int ConfigurationPanelHeight;
		}

		[Flags]
		public enum ReceiveNotificationFlags
		{
			StartupOnly = 0,
			PlayerEvents = 1,
			DataStreamEvents = 2,
			TagEvents = 4
		}

		public enum NotificationType
		{
			PluginStartup,
			TrackChanging = 16,
			TrackChanged = 1,
			PlayStateChanged,
			AutoDjStarted,
			AutoDjStopped,
			VolumeMuteChanged,
			VolumeLevelChanged,
			NowPlayingListChanged,
			NowPlayingListEnded = 18,
			NowPlayingArtworkReady = 8,
			NowPlayingLyricsReady,
			TagsChanging,
			TagsChanged,
			RatingChanging = 15,
			RatingChanged = 12,
			PlayCountersChanged,
			ScreenSaverActivating,
			ShutdownStarted = 17,
			EmbedInPanel = 19,
			PlayerRepeatChanged,
			PlayerShuffleChanged,
			PlayerEqualiserOnOffChanged,
			PlayerScrobbleChanged,
			ReplayGainChanged,
			FileDeleting,
			FileDeleted,
			ApplicationWindowChanged
		}

		public enum PluginCloseReason
		{
			MusicBeeClosing = 1,
			UserDisabled,
			StopNoUnload
		}

		public enum CallbackType
		{
			SettingsUpdated = 1,
			StorageReady,
			StorageFailed,
			FilesRetrievedChanged,
			FilesRetrievedNoChange,
			FilesRetrievedFail,
			LyricsDownloaded,
			StorageEject
		}

		public enum FilePropertyType
		{
			Url = 2,
			Kind = 4,
			Format,
			Size = 7,
			Channels,
			SampleRate,
			Bitrate,
			DateModified,
			DateAdded,
			LastPlayed,
			PlayCount,
			SkipCount,
			Duration,
			NowPlayingListIndex = 78,
			ReplayGainTrack = 94,
			ReplayGainAlbum
		}

		public enum MetaDataType
		{
			TrackTitle = 65,
			Album = 30,
			AlbumArtist,
			AlbumArtistRaw = 34,
			Artist = 32,
			MultiArtist,
			PrimaryArtist = 19,
			Artists = 144,
			ArtistsWithArtistRole,
			ArtistsWithPerformerRole,
			ArtistsWithGuestRole,
			ArtistsWithRemixerRole,
			Artwork = 40,
			BeatsPerMin,
			Composer = 43,
			MultiComposer = 89,
			Comment = 44,
			Conductor,
			Custom1,
			Custom2,
			Custom3,
			Custom4,
			Custom5,
			Custom6 = 96,
			Custom7,
			Custom8,
			Custom9,
			Custom10 = 128,
			Custom11,
			Custom12,
			Custom13,
			Custom14,
			Custom15,
			Custom16,
			DiscNo = 52,
			DiscCount = 54,
			Encoder,
			Genre = 59,
			Genres = 143,
			GenreCategory = 60,
			Grouping,
			Keywords = 84,
			HasLyrics = 63,
			Lyricist = 62,
			Lyrics = 114,
			Mood = 64,
			Occasion = 66,
			Origin,
			Publisher = 73,
			Quality,
			Rating,
			RatingLove,
			RatingAlbum = 104,
			Tempo = 85,
			TrackNo,
			TrackCount,
			Virtual1 = 109,
			Virtual2,
			Virtual3,
			Virtual4,
			Virtual5,
			Virtual6 = 122,
			Virtual7,
			Virtual8,
			Virtual9,
			Virtual10 = 135,
			Virtual11,
			Virtual12,
			Virtual13,
			Virtual14,
			Virtual15,
			Virtual16,
			Year = 88
		}

		[Flags]
		public enum LibraryCategory
		{
			Music = 0,
			Audiobook = 1,
			Video = 2,
			Inbox = 4
		}

		public enum DeviceIdType
		{
			GooglePlay = 1,
			AppleDevice,
			GooglePlay2,
			AppleDevice2
		}

		public enum DataType
		{
			String,
			Number,
			DateTime,
			Rating
		}

		public enum SettingId
		{
			CompactPlayerFlickrEnabled = 1,
			FileTaggingPreserveModificationTime
		}

		public enum ComparisonType
		{
			Is,
			IsSimilar = 20
		}

		public enum LyricsType
		{
			NotSpecified,
			Synchronised,
			UnSynchronised
		}

		public enum PlayState
		{
			Undefined,
			Loading,
			Playing = 3,
			Paused = 6,
			Stopped
		}

		public enum RepeatMode
		{
			None,
			All,
			One
		}

		public enum PlayButtonType
		{
			PreviousTrack,
			PlayPause,
			NextTrack,
			Stop
		}

		public enum PlaylistFormat
		{
			Unknown,
			M3u,
			Xspf,
			Asx,
			Wpl,
			Pls,
			Auto = 7,
			M3uAscii,
			AsxFile,
			Radio,
			M3uExtended,
			Mbp
		}

		public enum SkinElement
		{
			SkinInputControl = 7,
			SkinInputPanel = 10,
			SkinInputPanelLabel = 14,
			SkinTrackAndArtistPanel = -1
		}

		public enum ElementState
		{
			ElementStateDefault,
			ElementStateModified = 6
		}

		public enum ElementComponent
		{
			ComponentBorder,
			ComponentBackground,
			ComponentForeground = 3
		}

		public enum PluginPanelDock
		{
			ApplicationWindow,
			TrackAndArtistPanel
		}

		public enum ReplayGainMode
		{
			Off,
			Track,
			Album,
			Smart
		}

		public enum Command
		{
			NavigateTo = 1
		}

		public delegate void MB_ReleaseStringDelegate(string p1);

		public delegate void MB_TraceDelegate(string p1);

		public delegate IntPtr MB_WindowHandleDelegate();

		public delegate void MB_RefreshPanelsDelegate();

		public delegate void MB_SendNotificationDelegate(Plugin.CallbackType type);

		public delegate ToolStripItem MB_AddMenuItemDelegate(string menuPath, string hotkeyDescription, EventHandler handler);

		public delegate void MB_RegisterCommandDelegate(string command, EventHandler handler);

		public delegate void MB_CreateBackgroundTaskDelegate(ThreadStart taskCallback, Form owner);

		public delegate void MB_CreateParameterisedBackgroundTaskDelegate(ParameterizedThreadStart taskCallback, object parameters, Form owner);

		public delegate void MB_SetBackgroundTaskMessageDelegate(string message);

		public delegate Rectangle MB_GetPanelBoundsDelegate(Plugin.PluginPanelDock dock);

		public delegate bool MB_SetPanelScrollableAreaDelegate(Control panel, Size scrollArea, bool alwaysShowScrollBar);

		public delegate Control MB_AddPanelDelegate(Control panel, Plugin.PluginPanelDock dock);

		public delegate void MB_RemovePanelDelegate(Control panel);

		public delegate string MB_GetLocalisationDelegate(string id, string defaultText);

		public delegate bool MB_ShowNowPlayingAssistantDelegate();

		public delegate bool MB_InvokeCommandDelegate(Plugin.Command command, object parameter);

		public delegate bool MB_OpenFilterInTabDelegate(Plugin.MetaDataType field1, Plugin.ComparisonType comparison1, string value1, Plugin.MetaDataType field2, Plugin.ComparisonType comparison2, string value2);

		public delegate bool MB_SetWindowSizeDelegate(int width, int height);

		public delegate string Setting_GetFieldNameDelegate(Plugin.MetaDataType field);

		public delegate string Setting_GetPersistentStoragePathDelegate();

		public delegate string Setting_GetSkinDelegate();

		public delegate int Setting_GetSkinElementColourDelegate(Plugin.SkinElement element, Plugin.ElementState state, Plugin.ElementComponent component);

		public delegate bool Setting_IsWindowBordersSkinnedDelegate();

		public delegate Font Setting_GetDefaultFontDelegate();

		public delegate Plugin.DataType Setting_GetDataTypeDelegate(Plugin.MetaDataType field);

		public delegate string Setting_GetLastFmUserIdDelegate();

		public delegate string Setting_GetWebProxyDelegate();

		public delegate bool Setting_GetValueDelegate(Plugin.SettingId settingId, ref object value);

		public delegate string Library_GetFilePropertyDelegate(string sourceFileUrl, Plugin.FilePropertyType type);

		public delegate string Library_GetFileTagDelegate(string sourceFileUrl, Plugin.MetaDataType field);

		public delegate bool Library_SetFileTagDelegate(string sourceFileUrl, Plugin.MetaDataType field, string value);

		public delegate string Library_GetDevicePersistentIdDelegate(string sourceFileUrl, Plugin.DeviceIdType idType);

		public delegate bool Library_SetDevicePersistentIdDelegate(string sourceFileUrl, Plugin.DeviceIdType idType, string value);

		public delegate bool Library_FindDevicePersistentIdDelegate(Plugin.DeviceIdType idType, string[] ids, ref string[] values);

		public delegate bool Library_CommitTagsToFileDelegate(string sourceFileUrl);

		public delegate string Library_AddFileToLibraryDelegate(string sourceFileUrl, Plugin.LibraryCategory category);

		public delegate bool Library_GetSyncDeltaDelegate(string[] cachedFiles, DateTime updatedSince, Plugin.LibraryCategory categories, ref string[] newFiles, ref string[] updatedFiles, ref string[] deletedFiles);

		public delegate string Library_GetLyricsDelegate(string sourceFileUrl, Plugin.LyricsType type);

		public delegate string Library_GetArtworkDelegate(string sourceFileUrl, int index);

		public delegate string Library_GetArtistPictureDelegate(string artistName, int fadingPercent, int fadingColor);

		public delegate bool Library_GetArtistPictureUrlsDelegate(string artistName, bool localOnly, ref string[] urls);

		public delegate string Library_GetArtistPictureThumbDelegate(string artistName);

		public delegate bool Library_QueryFilesDelegate(string query);

		public delegate string Library_QueryGetNextFileDelegate();

		public delegate string Library_QueryGetAllFilesDelegate();

		public delegate bool Library_QueryFilesExDelegate(string query, ref string[] files);

		public delegate string Library_QuerySimilarArtistsDelegate(string artistName, double minimumArtistSimilarityRating);

		public delegate bool Library_QueryLookupTableDelegate(string keyTags, string valueTags, string query);

		public delegate string Library_QueryGetLookupTableValueDelegate(string key);

		public delegate int Player_GetPositionDelegate();

		public delegate bool Player_SetPositionDelegate(int position);

		public delegate Plugin.PlayState Player_GetPlayStateDelegate();

		public delegate bool Player_GetButtonEnabledDelegate(Plugin.PlayButtonType button);

		public delegate bool Player_ActionDelegate();

		public delegate int Player_QueueRandomTracksDelegate(int count);

		public delegate float Player_GetVolumeDelegate();

		public delegate bool Player_SetVolumeDelegate(float volume);

		public delegate bool Player_GetMuteDelegate();

		public delegate bool Player_SetMuteDelegate(bool mute);

		public delegate bool Player_GetShuffleDelegate();

		public delegate bool Player_SetShuffleDelegate(bool shuffle);

		public delegate Plugin.RepeatMode Player_GetRepeatDelegate();

		public delegate bool Player_SetRepeatDelegate(Plugin.RepeatMode repeat);

		public delegate bool Player_GetEqualiserEnabledDelegate();

		public delegate bool Player_SetEqualiserEnabledDelegate(bool enabled);

		public delegate bool Player_GetDspEnabledDelegate();

		public delegate bool Player_SetDspEnabledDelegate(bool enabled);

		public delegate bool Player_GetScrobbleEnabledDelegate();

		public delegate bool Player_SetScrobbleEnabledDelegate(bool enabled);

		public delegate bool Player_GetShowTimeRemainingDelegate();

		public delegate bool Player_GetShowRatingTrackDelegate();

		public delegate bool Player_GetShowRatingLoveDelegate();

		public delegate bool Player_ShowEqualiserDelegate();

		public delegate bool Player_GetAutoDjEnabledDelegate();

		public delegate bool Player_GetStopAfterCurrentEnabledDelegate();

		public delegate bool Player_GetCrossfadeDelegate();

		public delegate bool Player_SetCrossfadeDelegate(bool crossfade);

		public delegate Plugin.ReplayGainMode Player_GetReplayGainModeDelegate();

		public delegate bool Player_SetReplayGainModeDelegate(Plugin.ReplayGainMode mode);

		public delegate string NowPlaying_GetFileUrlDelegate();

		public delegate int NowPlaying_GetDurationDelegate();

		public delegate string NowPlaying_GetFilePropertyDelegate(Plugin.FilePropertyType type);

		public delegate string NowPlaying_GetFileTagDelegate(Plugin.MetaDataType field);

		public delegate string NowPlaying_GetLyricsDelegate();

		public delegate string NowPlaying_GetArtworkDelegate();

		public delegate string NowPlaying_GetArtistPictureDelegate(int fadingPercent);

		public delegate bool NowPlaying_GetArtistPictureUrlsDelegate(bool localOnly, ref string[] urls);

		public delegate string NowPlaying_GetArtistPictureThumbDelegate();

		public delegate bool NowPlaying_IsSoundtrackDelegate();

		public delegate int NowPlaying_GetSpectrumDataDelegate(float[] fftData);

		public delegate bool NowPlaying_GetSoundGraphDelegate(float[] graphData);

		public delegate int NowPlayingList_GetCurrentIndexDelegate();

		public delegate int NowPlayingList_GetNextIndexDelegate(int offset);

		public delegate bool NowPlayingList_IsAnyPriorTracksDelegate();

		public delegate bool NowPlayingList_IsAnyFollowingTracksDelegate();

		public delegate string NowPlayingList_GetFileUrlDelegate(int index);

		public delegate string NowPlayingList_GetFilePropertyDelegate(int index, Plugin.FilePropertyType type);

		public delegate string NowPlayingList_GetFileTagDelegate(int index, Plugin.MetaDataType field);

		public delegate bool NowPlayingList_ActionDelegate();

		public delegate bool NowPlayingList_FileActionDelegate(string sourceFileUrl);

		public delegate bool NowPlayingList_FilesActionDelegate(string[] sourceFileUrl);

		public delegate bool NowPlayingList_RemoveAtDelegate(int index);

		public delegate bool NowPlayingList_MoveFilesDelegate(int[] fromIndices, int toIndex);

		public delegate string Playlist_GetNameDelegate(string playlistUrl);

		public delegate Plugin.PlaylistFormat Playlist_GetTypeDelegate(string playlistUrl);

		public delegate bool Playlist_QueryPlaylistsDelegate();

		public delegate string Playlist_QueryGetNextPlaylistDelegate();

		public delegate bool Playlist_IsInListDelegate(string playlistUrl, string filename);

		public delegate bool Playlist_QueryFilesDelegate(string playlistUrl);

		public delegate bool Playlist_QueryFilesExDelegate(string playlistUrl, ref string[] filenames);

		public delegate string Playlist_CreatePlaylistDelegate(string folderName, string playlistName, string[] filenames);

		public delegate bool Playlist_DeletePlaylistDelegate(string playlistUrl);

		public delegate bool Playlist_SetFilesDelegate(string playlistUrl, string[] filenames);

		public delegate bool Playlist_AddFilesDelegate(string playlistUrl, string[] filenames);

		public delegate bool Playlist_RemoveAtDelegate(string playlistUrl, int index);

		public delegate bool Playlist_MoveFilesDelegate(string playlistUrl, int[] fromIndices, int toIndex);

		public delegate bool Playlist_PlayNowDelegate(string playlistUrl);

		public delegate string Pending_GetFileUrlDelegate();

		public delegate string Pending_GetFilePropertyDelegate(Plugin.FilePropertyType field);

		public delegate string Pending_GetFileTagDelegate(Plugin.MetaDataType field);

		public delegate string Sync_FileStartDelegate(string filename);

		public delegate void Sync_FileEndDelegate(string filename, bool success, string errorMessage);

		public const short PluginInfoVersion = 1;

		public const short MinInterfaceVersion = 29;

		public const short MinApiRevision = 33;

		private Plugin.MusicBeeApiInterface mbApi;

		private Plugin.PluginInfo about = new Plugin.PluginInfo();

		private IPCInterface ipcInterface;

		private Thread interfaceThread;

		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		private static extern void CopyMemory(ref Plugin.MusicBeeApiInterface mbApiInterface, IntPtr src, int length);

		public Plugin.PluginInfo Initialise(IntPtr apiInterfacePtr)
		{
			this.mbApi = default(Plugin.MusicBeeApiInterface);
			this.mbApi.Initialise(apiInterfacePtr);
			this.about.PluginInfoVersion = 1;
			this.about.Name = "MusicBeeIPC";
			this.about.Description = "Enables controlling MusicBee with Windows Messages";
			this.about.TargetApplication = "General";
			this.about.Author = "Kerli Low";
			this.about.Type = Plugin.PluginType.General;
			this.about.VersionMajor = 2;
			this.about.VersionMinor = 0;
			this.about.Revision = 1;
			this.about.MinInterfaceVersion = 29;
			this.about.MinApiRevision = 33;
			this.about.ReceiveNotifications = Plugin.ReceiveNotificationFlags.StartupOnly;
			this.about.ConfigurationPanelHeight = 0;
			this.interfaceThread = new Thread(new ThreadStart(this.RunIPCInterface));
			this.interfaceThread.Start();
			return this.about;
		}

		public bool Configure(IntPtr panelHandle)
		{
			About about = new About();
			about.Show();
			return true;
		}

		public void SaveSettings()
		{
			this.mbApi.Setting_GetPersistentStoragePath();
		}

		public void Close(Plugin.PluginCloseReason reason)
		{
			this.ipcInterface.Close();
			Application.Exit();
			this.interfaceThread.Join();
		}

		public void Uninstall()
		{
		}

		public void ReceiveNotification(string sourceFileUrl, Plugin.NotificationType type)
		{
		}

		public void RunIPCInterface()
		{
			this.ipcInterface = new IPCInterface(ref this.mbApi);
			Application.Run();
		}
	}
}
