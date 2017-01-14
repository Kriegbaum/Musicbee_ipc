//--------------------------------------------------------//
// MusicBeeIPCSDK C++ v2.0.0                              //
// Copyright © Kerli Low 2014                             //
// This file is licensed under the                        //
// BSD 2-Clause License                                   //
// See LICENSE_MusicBeeIPCSDK for more information.       //
//--------------------------------------------------------//

#ifndef MUSICBEEIPCI_H
#define MUSICBEEIPCI_H

#include <string>
#include <vector>
#include <Windows.h>

#include "Enums.h"
#include "Structs.h"


class MusicBeeIPC
{
public:
    MusicBeeIPC();

    // Test if MusicBeeIPC hwnd is valid
    // Returns true if MusicBeeIPC responded, false otherwise
    bool probe() const;

    MBError playPause       () const;
    MBError play            () const;
    MBError pause           () const;
    MBError stop            () const;
    MBError stopAfterCurrent() const;
    MBError previousTrack   () const;
    MBError nextTrack       () const;
    MBError startAutoDj     () const;
    MBError endAutoDj       () const;

    MBPlayState  getPlayState   () const;
    std::wstring getPlayStateStr() const;

    int     getPosition()             const;
    MBError setPosition(int position) const;

    // Volume: Value between 0 - 100
    int     getVolume()              const;
    MBError setVolume(int volume)    const;
    // Precise volume: Value between 0 - 10000
    int     getVolumep()             const;
    MBError setVolumep(int volume)   const;
    // Floating point volume: Value between 0.0 - 1.0
    float   getVolumef()             const;
    MBError setVolumef(float volume) const;

    bool         getMute   ()          const;
    MBError      setMute   (bool mute) const;
    bool         getShuffle()             const;
    MBError      setShuffle(bool shuffle) const;
    MBRepeatMode getRepeat ()                    const;
    MBError      setRepeat (MBRepeatMode repeat) const;

    bool    getEqualiserEnabled()             const;
    MBError setEqualiserEnabled(bool enabled) const;
    bool    getDspEnabled      ()             const;
    MBError setDspEnabled      (bool enabled) const;
    bool    getScrobbleEnabled ()             const;
    MBError setScrobbleEnabled (bool enabled) const;

    MBError showEqualiser() const;

    bool getAutoDjEnabled() const;
    
    bool    getStopAfterCurrentEnabled()             const;
    MBError setStopAfterCurrentEnabled(bool enabled) const;
    
    bool             getCrossfade     ()                      const;
    MBError          setCrossfade     (bool crossfade)        const;
    MBReplayGainMode getReplayGainMode()                      const;
    MBError          setReplayGainMode(MBReplayGainMode mode) const;

    MBError queueRandomTracks(int count) const;

    int getDuration() const;

    std::wstring getFileUrl              ()                                                const;
    std::wstring getFileProperty         (MBFileProperty fileProperty)                     const;
    std::wstring getFileTag              (MBMetaData     field)                            const;
    std::wstring getLyrics               ()                                                const;
    std::wstring getDownloadedLyrics     ()                                                const;
    std::wstring getArtwork              ()                                                const;
    std::wstring getArtworkUrl           ()                                                const;
    std::wstring getDownloadedArtwork    ()                                                const;
    std::wstring getDownloadedArtworkUrl ()                                                const;
    std::wstring getArtistPicture        (int fadingPercent)                               const;
    MBError      getArtistPictureUrls    (bool localOnly, std::vector<std::wstring>& urls) const;
    std::wstring getArtistPictureThumb   ()                                                const;
    bool         isSoundtrack            ()                                                const;
    MBError      getSoundtrackPictureUrls(bool localOnly, std::vector<std::wstring>& urls) const;
    
    int getCurrentIndex()           const;
    int getNextIndex   (int offset) const;

    bool isAnyPriorTracks    () const;
    bool isAnyFollowingTracks() const;

    MBError playNow  (const std::wstring& fileurl) const;
    MBError queueNext(const std::wstring& fileurl) const;
    MBError queueLast(const std::wstring& fileurl) const;

    MBError clearNowPlayingList()          const;
    MBError removeAt           (int index) const;

    MBError moveFiles(const std::vector<int>& fromIndices, int toIndex) const;

    MBError showNowPlayingAssistant() const;

    bool getShowTimeRemaining() const;
    bool getShowRatingTrack  () const;
    bool getShowRatingLove   () const;

    bool getButtonEnabled(MBPlayButtonType button) const;

    MBError      jump              (int index)                                                                  const;
    MBError      search            (const std::wstring& query, std::vector<std::wstring>& result)               const;
    MBError      search            (const std::wstring& query, const std::wstring& comparison,
                                    const std::vector<std::wstring>& fields, std::vector<std::wstring>& result) const;
    std::wstring searchFirst       (const std::wstring& query)                                                  const;
    std::wstring searchFirst       (const std::wstring& query, const std::wstring& comparison,
                                    const std::vector<std::wstring>& fields)                                    const;
    MBError      searchIndices     (const std::wstring& query, std::vector<int>& result)                        const;
    MBError      searchIndices     (const std::wstring& query, const std::wstring& comparison,
                                    const std::vector<std::wstring>& fields, std::vector<int>& result)          const;
    int          searchFirstIndex  (const std::wstring& query)                                                  const;
    int          searchFirstIndex  (const std::wstring& query, const std::wstring& comparison,
                                    const std::vector<std::wstring>& fields)                                    const;
    MBError      searchAndPlayFirst(const std::wstring& query)                                                  const;
    MBError      searchAndPlayFirst(const std::wstring& query, const std::wstring& comparison,
                                    const std::vector<std::wstring>& fields)                                    const;

    std::wstring nowPlayingList_getListFileUrl (int index)                              const;
    std::wstring nowPlayingList_getFileProperty(int index, MBFileProperty fileProperty) const;
    std::wstring nowPlayingList_getFileTag     (int index, MBMetaData field)            const;

    MBError      nowPlayingList_queryFiles      (const std::wstring& query)                                    const;
    std::wstring nowPlayingList_queryGetNextFile()                                                             const;
    std::wstring nowPlayingList_queryGetAllFiles()                                                             const;
    MBError      nowPlayingList_queryFilesEx    (const std::wstring& query, std::vector<std::wstring>& result) const;

    MBError nowPlayingList_playLibraryShuffled() const;

    int nowPlayingList_getItemCount() const;

    std::wstring     playlist_getName(const std::wstring& playlistUrl) const;
    MBPlaylistFormat playlist_getType(const std::wstring& playlistUrl) const;

    bool playlist_isInList(const std::wstring& playlistUrl, const std::wstring& filename) const;

    MBError      playlist_queryPlaylists      () const;
    std::wstring playlist_queryGetNextPlaylist() const;

    MBError      playlist_queryFiles(const std::wstring& playlistUrl)                                      const;
    std::wstring playlist_queryGetNextFile()                                                               const;
    std::wstring playlist_queryGetAllFiles()                                                               const;
    MBError      playlist_queryFilesEx(const std::wstring& playlistUrl, std::vector<std::wstring>& result) const;

    std::wstring playlist_createPlaylist(const std::wstring& folderName, const std::wstring& playlistName,
                                         const std::vector<std::wstring>& filenames)                       const;
    MBError      playlist_deletePlaylist(const std::wstring& playlistUrl)                                  const;

    MBError playlist_setFiles   (const std::wstring& playlistUrl, const std::vector<std::wstring>& filenames) const;
    MBError playlist_appendFiles(const std::wstring& playlistUrl, const std::vector<std::wstring>& filenames) const;

    MBError playlist_removeAt(const std::wstring& playlistUrl, int index) const;

    MBError playlist_moveFiles(const std::wstring& playlistUrl, const std::vector<int>& fromIndices, int toIndex) const;

    MBError playlist_playNow(const std::wstring& playlistUrl) const;

    int playlist_getItemCount(const std::wstring& playlistUrl) const;

    std::wstring library_getFileProperty      (const std::wstring& fileUrl, MBFileProperty fileProperty) const;
    std::wstring library_getFileTag           (const std::wstring& fileUrl, MBMetaData field)            const;
    MBError      library_setFileTag           (const std::wstring& fileUrl, MBMetaData field,
                                               const std::wstring& value)                                const;
    MBError      library_commitTagsToFile     (const std::wstring& fileUrl)                              const;
    std::wstring library_getLyrics            (const std::wstring& fileUrl, MBLyricsType type)           const;
    std::wstring library_getArtwork           (const std::wstring& fileUrl, int index)                   const;
    std::wstring library_getArtworkUrl        (const std::wstring& fileUrl, int index)                   const;
    std::wstring library_getArtistPicture     (const std::wstring& artistName,
                                               int fadingPercent, int fadingColor)                       const;
    MBError      library_getArtistPictureUrls (const std::wstring& artistName, bool localOnly,
                                               std::vector<int>& urls)                                   const;
    std::wstring library_getArtistPictureThumb(const std::wstring& artistName)                           const;

    std::wstring library_addFileToLibrary(const std::wstring& fileUrl, MBLibraryCategory category) const;

    MBError      library_queryFiles              (const std::wstring& query)                                    const;
    std::wstring library_queryGetNextFile        ()                                                             const;
    std::wstring library_queryGetAllFiles        ()                                                             const;
    MBError      library_queryFilesEx            (const std::wstring& query, std::vector<std::wstring>& result) const;
    std::wstring library_querySimilarArtists     (const std::wstring& artistName,
                                                  double minimumArtistSimilarityRating)                         const;
    MBError      library_queryLookupTable        (const std::wstring& keyTags, const std::wstring& valueTags,
                                                  const std::wstring& query)                                    const;
    std::wstring library_queryGetLookupTableValue(const std::wstring& key)                                      const;

    MBError      library_jump              (int index)                                                         const;
    MBError      library_search            (const std::wstring& query, std::vector<std::wstring>& result)      const;
    MBError      library_search            (const std::wstring& query, const std::wstring& comparison,
                                            const std::vector<std::wstring>& fields,
                                            std::vector<std::wstring>& result)                                 const;
    std::wstring library_searchFirst       (const std::wstring& query)                                         const;
    std::wstring library_searchFirst       (const std::wstring& query, const std::wstring& comparison,
                                            const std::vector<std::wstring>& fields)                           const;
    MBError      library_searchIndices     (const std::wstring& query, std::vector<int>& result)               const;
    MBError      library_searchIndices     (const std::wstring& query, const std::wstring& comparison,
                                            const std::vector<std::wstring>& fields, std::vector<int>& result) const;
    int          library_searchFirstIndex  (const std::wstring& query)                                         const;
    int          library_searchFirstIndex  (const std::wstring& query, const std::wstring& comparison,
                                            const std::vector<std::wstring>& fields)                           const;
    MBError      library_searchAndPlayFirst(const std::wstring& query)                                         const;
    MBError      library_searchAndPlayFirst(const std::wstring& query, const std::wstring& comparison,
                                            const std::vector<std::wstring>& fields)                           const;

    std::wstring setting_getFieldName(MBMetaData field) const;
    MBDataType   setting_getDataType (MBMetaData field) const;

    HWND window_getHandle() const;

    MBError window_close   () const;
    MBError window_restore () const;
    MBError window_minimize() const;
    MBError window_maximize() const;

    MBError window_move  (int x, int y) const;
    MBError window_resize(int w, int h) const;

    MBError window_bringToFront() const;

    MBError window_getPosition(int& x, int& y) const;
    MBError window_getSize    (int& w, int& h) const;

    MBMusicBeeVersion getMusicBeeVersion   ()                       const;
    std::wstring      getMusicBeeVersionStr()                       const;
    std::wstring      getPluginVersionStr  ()                       const;
    MBError           getPluginVersion     (int& major, int& minor) const;

    MBError test      ()                                                      const;
    MBError messageBox(const std::wstring& text, const std::wstring& caption) const;

private:    
    HWND findHwnd() const;

    LPARAM toLPARAM(bool b)    const;
    bool   toBool  (LRESULT r) const;
    
    void free(COPYDATASTRUCT& cds) const;
    
    COPYDATASTRUCT pack(int int32_1, int int32_2) const;
    COPYDATASTRUCT pack(const std::wstring& string_1) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, const std::wstring& string_2) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, const std::wstring& string_2, const std::wstring& string_3) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, int int32_1) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, int int32_1, int int32_2) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, bool bool_1) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, double double_1) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, const std::vector<std::wstring>& strings) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, const std::wstring& string_2,
                        const std::vector<std::wstring>& strings) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, int int32_1, const std::wstring& string_2) const;
    COPYDATASTRUCT pack(const std::vector<int>& int32s, int int32_1) const;
    COPYDATASTRUCT pack(const std::wstring& string_1, const std::vector<int>& int32s, int int32_1) const;

    bool unpack(LRESULT lr, std::wstring& string_1) const;
    bool unpack(LRESULT lr, std::vector<std::wstring>& strings) const;
    bool unpack(LRESULT lr, int& int32_1, int& int32_2) const;
    bool unpack(LRESULT lr, std::vector<int>& int32s) const;

    HANDLE OpenMmf     (LRESULT lr) const;
    void   CloseMmf    (HANDLE mmf) const;
    void*  MapMmfView  (HANDLE mmf, LRESULT lr, void*& ptr) const;
    void   UnmapMmfView(const void* view) const;
};


#endif // MUSICBEEIPCI_H
