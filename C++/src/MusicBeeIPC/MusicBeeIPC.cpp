//--------------------------------------------------------//
// MusicBeeIPCSDK C++ v2.0.0                              //
// Copyright © Kerli Low 2014                             //
// This file is licensed under the                        //
// BSD 2-Clause License                                   //
// See LICENSE_MusicBeeIPCSDK for more information.       //
//--------------------------------------------------------//

#include <sstream>

#include "MusicBeeIPC/MusicBeeIPC.h"


MusicBeeIPC::MusicBeeIPC()
{
}

HWND MusicBeeIPC::findHwnd() const
{
    return FindWindowW(NULL, L"MusicBee IPC Interface");
}

bool MusicBeeIPC::probe() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Probe, 0)) != MBE_Error;
}

MBError MusicBeeIPC::playPause() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_PlayPause, 0));
}

MBError MusicBeeIPC::play() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Play, 0));
}

MBError MusicBeeIPC::pause() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Pause, 0));
}

MBError MusicBeeIPC::stop() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Stop, 0));
}

MBError MusicBeeIPC::stopAfterCurrent() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_StopAfterCurrent, 0));
}

MBError MusicBeeIPC::previousTrack() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_PreviousTrack, 0));
}

MBError MusicBeeIPC::nextTrack() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_NextTrack, 0));
}

MBError MusicBeeIPC::startAutoDj() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_StartAutoDj, 0));
}

MBError MusicBeeIPC::endAutoDj() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_EndAutoDj, 0));
}

MBPlayState MusicBeeIPC::getPlayState() const
{
    return static_cast<MBPlayState>(SendMessage(findHwnd(), WM_USER, MBC_GetPlayState, 0));
}

std::wstring MusicBeeIPC::getPlayStateStr() const
{
    switch (static_cast<MBPlayState>(SendMessage(findHwnd(), WM_USER, MBC_GetPlayState, 0)))
    {
        case MBPS_Loading:
            return L"Loading";
        case MBPS_Playing:
            return L"Playing";
        case MBPS_Paused:
            return L"Paused";
        case MBPS_Stopped:
            return L"Stopped";
        default:
            return L"Undefined";
    }
}

int MusicBeeIPC::getPosition() const
{
    return static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_GetPosition, 0));
}

MBError MusicBeeIPC::setPosition(int position) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetPosition, position));
}

int MusicBeeIPC::getVolume() const
{
    return static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_GetVolume, 0));
}

MBError MusicBeeIPC::setVolume(int volume) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetVolume, volume));
}

int MusicBeeIPC::getVolumep() const
{
    return static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_GetVolumep, 0));
}

MBError MusicBeeIPC::setVolumep(int volume) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetVolumep, volume));
}

float MusicBeeIPC::getVolumef() const
{
    return MBFloatInt(static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_GetVolumef, 0))).f;
}

MBError MusicBeeIPC::setVolumef(float volume) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetVolumef, MBFloatInt(volume).i));
}

bool MusicBeeIPC::getMute() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetMute, 0));
}

MBError MusicBeeIPC::setMute(bool mute) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetMute, toLPARAM(mute)));
}

bool MusicBeeIPC::getShuffle() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetShuffle, 0));
}

MBError MusicBeeIPC::setShuffle(bool shuffle) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetShuffle, toLPARAM(shuffle)));
}

MBRepeatMode MusicBeeIPC::getRepeat() const
{
    return static_cast<MBRepeatMode>(SendMessage(findHwnd(), WM_USER, MBC_GetRepeat, 0));
}

MBError MusicBeeIPC::setRepeat(MBRepeatMode repeat) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetRepeat, static_cast<LPARAM>(repeat)));
}

bool MusicBeeIPC::getEqualiserEnabled() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetEqualiserEnabled, 0));
}

MBError MusicBeeIPC::setEqualiserEnabled(bool enabled) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetEqualiserEnabled, toLPARAM(enabled)));
}

bool MusicBeeIPC::getDspEnabled() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetDspEnabled, 0));
}

MBError MusicBeeIPC::setDspEnabled(bool enabled) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetDspEnabled, toLPARAM(enabled)));
}

bool MusicBeeIPC::getScrobbleEnabled() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetScrobbleEnabled, 0));
}

MBError MusicBeeIPC::setScrobbleEnabled(bool enabled) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetScrobbleEnabled, toLPARAM(enabled)));
}

MBError MusicBeeIPC::showEqualiser() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_ShowEqualiser, 0));
}

bool MusicBeeIPC::getAutoDjEnabled() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetAutoDjEnabled, 0));
}

bool MusicBeeIPC::getStopAfterCurrentEnabled() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetStopAfterCurrentEnabled, 0));
}

MBError MusicBeeIPC::setStopAfterCurrentEnabled(bool enabled) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetStopAfterCurrentEnabled, toLPARAM(enabled)));
}
 
bool MusicBeeIPC::getCrossfade() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetCrossfade, 0));
}

MBError MusicBeeIPC::setCrossfade(bool crossfade) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetCrossfade, toLPARAM(crossfade)));
}

MBReplayGainMode MusicBeeIPC::getReplayGainMode() const
{
    return static_cast<MBReplayGainMode>(SendMessage(findHwnd(), WM_USER, MBC_GetReplayGainMode, 0));
}

MBError MusicBeeIPC::setReplayGainMode(MBReplayGainMode mode) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_SetReplayGainMode, static_cast<LPARAM>(mode)));
}

MBError MusicBeeIPC::queueRandomTracks(int count) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_QueueRandomTracks, static_cast<LPARAM>(count)));
}

int MusicBeeIPC::getDuration() const
{
    return static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_GetDuration, 0));
}

std::wstring MusicBeeIPC::getFileUrl() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetFileUrl, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getFileProperty(MBFileProperty fileProperty) const
{
    std::wstring r;
    
    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetFileProperty, fileProperty);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getFileTag(MBMetaData field) const
{
    std::wstring r;
    
    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetFileTag, field);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getLyrics() const
{
    std::wstring r;
    
    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetLyrics, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getDownloadedLyrics() const
{
    std::wstring r;
    
    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetDownloadedLyrics, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getArtwork() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetArtwork, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getArtworkUrl() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetArtworkUrl, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getDownloadedArtwork() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetDownloadedArtwork, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getDownloadedArtworkUrl() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetDownloadedArtworkUrl, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getArtistPicture(int fadingPercent) const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetArtistPicture, fadingPercent);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

MBError MusicBeeIPC::getArtistPictureUrls(bool localOnly, std::vector<std::wstring>& urls) const
{
    MBError r = MBE_Error;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetArtistPictureUrls, toLPARAM(localOnly));

    if (unpack(lr, urls))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::getArtistPictureThumb() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetArtistPictureThumb, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

bool MusicBeeIPC::isSoundtrack() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_IsSoundtrack, 0));
}

MBError MusicBeeIPC::getSoundtrackPictureUrls(bool localOnly, std::vector<std::wstring>& urls) const
{
    MBError r = MBE_Error;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_GetSoundtrackPictureUrls, toLPARAM(localOnly));

    if (unpack(lr, urls))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

int MusicBeeIPC::getCurrentIndex() const
{
    return static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_GetCurrentIndex, 0));
}

int MusicBeeIPC::getNextIndex(int offset) const
{
    return static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_GetNextIndex, offset));
}

bool MusicBeeIPC::isAnyPriorTracks() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_IsAnyPriorTracks, 0));
}

bool MusicBeeIPC::isAnyFollowingTracks() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_IsAnyFollowingTracks, 0));
}

MBError MusicBeeIPC::playNow(const std::wstring& fileurl) const
{
    COPYDATASTRUCT cds = pack(fileurl);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_PlayNow, reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::queueNext(const std::wstring& fileurl) const
{
    COPYDATASTRUCT cds = pack(fileurl);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_QueueNext,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::queueLast(const std::wstring& fileurl) const
{
    COPYDATASTRUCT cds = pack(fileurl);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_QueueLast,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::clearNowPlayingList() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_ClearNowPlayingList, 0));
}

MBError MusicBeeIPC::removeAt(int index) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_RemoveAt, index));
}

MBError MusicBeeIPC::moveFiles(const std::vector<int>& fromIndices, int toIndex) const
{
    COPYDATASTRUCT cds = pack(fromIndices, toIndex);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_MoveFiles,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::showNowPlayingAssistant() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_ShowNowPlayingAssistant, 0));
}

bool MusicBeeIPC::getShowTimeRemaining() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetShowTimeRemaining, 0));
}

bool MusicBeeIPC::getShowRatingTrack() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetShowRatingTrack, 0));
}

bool MusicBeeIPC::getShowRatingLove() const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetShowRatingLove, 0));
}

bool MusicBeeIPC::getButtonEnabled(MBPlayButtonType button) const
{
    return toBool(SendMessage(findHwnd(), WM_USER, MBC_GetButtonEnabled, button));
}

MBError MusicBeeIPC::jump(int index) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Jump, index));
}

MBError MusicBeeIPC::search(const std::wstring& query, std::vector<std::wstring>& result) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return search(query, L"Contains", fields, result);
}

MBError MusicBeeIPC::search(const std::wstring& query, const std::wstring& comparison,
                            const std::vector<std::wstring>& fields, std::vector<std::wstring>& result) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Search, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, result))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::searchFirst(const std::wstring& query) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchFirst(query, L"Contains", fields);
}

std::wstring MusicBeeIPC::searchFirst(const std::wstring& query, const std::wstring& comparison,
                                      const std::vector<std::wstring>& fields) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_SearchFirst, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::searchIndices(const std::wstring& query, std::vector<int>& result) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchIndices(query, L"Contains", fields, result);
}

MBError MusicBeeIPC::searchIndices(const std::wstring& query, const std::wstring& comparison,
                                   const std::vector<std::wstring>& fields, std::vector<int>& result) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_SearchIndices, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, result))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

int MusicBeeIPC::searchFirstIndex(const std::wstring& query) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchFirstIndex(query, L"Contains", fields);
}

int MusicBeeIPC::searchFirstIndex(const std::wstring& query, const std::wstring& comparison,
                                  const std::vector<std::wstring>& fields) const
{
    int r = -1;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    r = static_cast<int>(SendMessage(findHwnd(), WM_COPYDATA, MBC_SearchFirstIndex, reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::searchAndPlayFirst(const std::wstring& query) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchAndPlayFirst(query, L"Contains", fields);
}

MBError MusicBeeIPC::searchAndPlayFirst(const std::wstring& query, const std::wstring& comparison,
                                        const std::vector<std::wstring>& fields) const
{
    COPYDATASTRUCT cds = pack(query, comparison, fields);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_SearchAndPlayFirst,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::nowPlayingList_getListFileUrl(int index) const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_NowPlayingList_GetListFileUrl, index);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::nowPlayingList_getFileProperty(int index, MBFileProperty fileProperty) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(index, fileProperty);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_NowPlayingList_GetFileProperty, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::nowPlayingList_getFileTag(int index, MBMetaData field) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(index, field);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_NowPlayingList_GetFileTag, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::nowPlayingList_queryFiles(const std::wstring& query) const
{
    COPYDATASTRUCT cds = pack(query);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_NowPlayingList_QueryFiles,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::nowPlayingList_queryGetNextFile() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_NowPlayingList_QueryGetNextFile, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::nowPlayingList_queryGetAllFiles() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_NowPlayingList_QueryGetAllFiles, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

MBError MusicBeeIPC::nowPlayingList_queryFilesEx(const std::wstring& query, std::vector<std::wstring>& result) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(query);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_NowPlayingList_QueryFilesEx, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, result))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::nowPlayingList_playLibraryShuffled() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_NowPlayingList_PlayLibraryShuffled, 0));
}

int MusicBeeIPC::nowPlayingList_getItemCount() const
{
    return static_cast<int>(SendMessage(findHwnd(), WM_USER, MBC_NowPlayingList_GetItemCount, 0));
}

std::wstring MusicBeeIPC::playlist_getName(const std::wstring& playlistUrl) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(playlistUrl);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Playlist_GetName, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBPlaylistFormat MusicBeeIPC::playlist_getType(const std::wstring& playlistUrl) const
{
    COPYDATASTRUCT cds = pack(playlistUrl);

    MBPlaylistFormat r = static_cast<MBPlaylistFormat>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_GetType,
                                                                   reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

bool MusicBeeIPC::playlist_isInList(const std::wstring& playlistUrl, const std::wstring& filename) const
{
    COPYDATASTRUCT cds = pack(playlistUrl, filename);

    bool r = toBool(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_IsInList, reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::playlist_queryPlaylists() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Playlist_QueryPlaylists, 0));
}

std::wstring MusicBeeIPC::playlist_queryGetNextPlaylist() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Playlist_QueryGetNextPlaylist, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

MBError MusicBeeIPC::playlist_queryFiles(const std::wstring& playlistUrl) const
{
    COPYDATASTRUCT cds = pack(playlistUrl);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_QueryFiles,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::playlist_queryGetNextFile() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Playlist_QueryGetNextFile, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::playlist_queryGetAllFiles() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Playlist_QueryGetAllFiles, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

MBError MusicBeeIPC::playlist_queryFilesEx(const std::wstring& playlistUrl, std::vector<std::wstring>& result) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(playlistUrl);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Playlist_QueryFilesEx, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, result))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::playlist_createPlaylist(const std::wstring& folderName, const std::wstring& playlistName,
                                                  const std::vector<std::wstring>& filenames) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(folderName, playlistName, filenames);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Playlist_CreatePlaylist, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::playlist_deletePlaylist(const std::wstring& playlistUrl) const
{
    COPYDATASTRUCT cds = pack(playlistUrl);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_DeletePlaylist,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::playlist_setFiles(const std::wstring& playlistUrl,
                                       const std::vector<std::wstring>& filenames) const
{
    COPYDATASTRUCT cds = pack(playlistUrl, filenames);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_SetFiles,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::playlist_appendFiles(const std::wstring& playlistUrl,
                                          const std::vector<std::wstring>& filenames) const
{
    COPYDATASTRUCT cds = pack(playlistUrl, filenames);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_AppendFiles,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::playlist_removeAt(const std::wstring& playlistUrl, int index) const
{
    COPYDATASTRUCT cds = pack(playlistUrl, index);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_RemoveAt,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::playlist_moveFiles(const std::wstring& playlistUrl,
                                        const std::vector<int>& fromIndices, int toIndex) const
{
    COPYDATASTRUCT cds = pack(playlistUrl, fromIndices, toIndex);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_MoveFiles,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::playlist_playNow(const std::wstring& playlistUrl) const
{
    COPYDATASTRUCT cds = pack(playlistUrl);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_PlayNow,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

int MusicBeeIPC::playlist_getItemCount(const std::wstring& playlistUrl) const
{
    COPYDATASTRUCT cds = pack(playlistUrl);

    int r = static_cast<int>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Playlist_GetItemCount,
                                         reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_getFileProperty(const std::wstring& fileUrl, MBFileProperty fileProperty) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(fileUrl, fileProperty);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetFileProperty, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_getFileTag(const std::wstring& fileUrl, MBMetaData field) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(fileUrl, field);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetFileTag, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::library_setFileTag(const std::wstring& fileUrl, MBMetaData field, const std::wstring& value) const
{
    COPYDATASTRUCT cds = pack(fileUrl, field, value);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Library_SetFileTag, reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::library_commitTagsToFile(const std::wstring& fileUrl) const
{
    COPYDATASTRUCT cds = pack(fileUrl);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Library_CommitTagsToFile,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_getLyrics(const std::wstring& fileUrl, MBLyricsType type) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(fileUrl, type);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetLyrics, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_getArtwork(const std::wstring& fileUrl, int index) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(fileUrl, index);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetArtwork, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_getArtworkUrl(const std::wstring& fileUrl, int index) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(fileUrl, index);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetArtworkUrl, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_getArtistPicture(const std::wstring& artistName,
                                                   int fadingPercent, int fadingColor) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(artistName, fadingPercent, fadingColor);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetArtistPicture, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::library_getArtistPictureUrls(const std::wstring& artistName, bool localOnly,
                                                  std::vector<int>& urls) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(artistName, localOnly);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetArtistPictureUrls, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, urls))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_getArtistPictureThumb(const std::wstring& artistName) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(artistName);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_GetArtistPictureThumb, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_addFileToLibrary(const std::wstring& fileUrl, MBLibraryCategory category) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(fileUrl, category);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_AddFileToLibrary, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::library_queryFiles(const std::wstring& query) const
{
    COPYDATASTRUCT cds = pack(query);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Library_QueryFiles,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_queryGetNextFile() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Library_QueryGetNextFile, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

std::wstring MusicBeeIPC::library_queryGetAllFiles() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Library_QueryGetAllFiles, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

MBError MusicBeeIPC::library_queryFilesEx(const std::wstring& query, std::vector<std::wstring>& result) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(query);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_QueryFilesEx, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, result))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_querySimilarArtists(const std::wstring& artistName,
                                                      double minimumArtistSimilarityRating) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(artistName, minimumArtistSimilarityRating);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_QuerySimilarArtists, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::library_queryLookupTable(const std::wstring& keyTags, const std::wstring& valueTags,
                                              const std::wstring& query) const
{
    COPYDATASTRUCT cds = pack(keyTags, valueTags, query);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Library_QueryLookupTable,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_queryGetLookupTableValue(const std::wstring& key) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(key);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_QueryGetLookupTableValue, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::library_jump(int index) const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Library_Jump, index));
}

MBError MusicBeeIPC::library_search(const std::wstring& query, std::vector<std::wstring>& result) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return search(query, L"Contains", fields, result);
}

MBError MusicBeeIPC::library_search(const std::wstring& query, const std::wstring& comparison,
                                    const std::vector<std::wstring>& fields, std::vector<std::wstring>& result) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_Search, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, result))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

std::wstring MusicBeeIPC::library_searchFirst(const std::wstring& query) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchFirst(query, L"Contains", fields);
}

std::wstring MusicBeeIPC::library_searchFirst(const std::wstring& query, const std::wstring& comparison,
                                              const std::vector<std::wstring>& fields) const
{
    std::wstring r;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_SearchFirst, reinterpret_cast<LPARAM>(&cds));

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

MBError MusicBeeIPC::library_searchIndices(const std::wstring& query, std::vector<int>& result) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchIndices(query, L"Contains", fields, result);
}

MBError MusicBeeIPC::library_searchIndices(const std::wstring& query, const std::wstring& comparison,
                                           const std::vector<std::wstring>& fields, std::vector<int>& result) const
{
    MBError r = MBE_Error;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_COPYDATA, MBC_Library_SearchIndices, reinterpret_cast<LPARAM>(&cds));

    if (unpack(lr, result))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    free(cds);

    return r;
}

int MusicBeeIPC::library_searchFirstIndex(const std::wstring& query) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchFirstIndex(query, L"Contains", fields);
}

int MusicBeeIPC::library_searchFirstIndex(const std::wstring& query, const std::wstring& comparison,
                                          const std::vector<std::wstring>& fields) const
{
    int r = -1;

    COPYDATASTRUCT cds = pack(query, comparison, fields);

    r = static_cast<int>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Library_SearchFirstIndex,
                                     reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::library_searchAndPlayFirst(const std::wstring& query) const
{
    std::vector<std::wstring> fields;
    fields.push_back(L"ArtistPeople");
    fields.push_back(L"Title");
    fields.push_back(L"Album");

    return searchAndPlayFirst(query, L"Contains", fields);
}

MBError MusicBeeIPC::library_searchAndPlayFirst(const std::wstring& query, const std::wstring& comparison,
                                                const std::vector<std::wstring>& fields) const
{
    COPYDATASTRUCT cds = pack(query, comparison, fields);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Library_SearchAndPlayFirst,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

std::wstring MusicBeeIPC::setting_getFieldName(MBMetaData field) const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Setting_GetFieldName, field);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

MBDataType MusicBeeIPC::setting_getDataType(MBMetaData field) const
{
    return static_cast<MBDataType>(SendMessage(findHwnd(), WM_USER, MBC_Setting_GetDataType, field));
}

HWND MusicBeeIPC::window_getHandle() const
{
    return reinterpret_cast<HWND>(SendMessage(findHwnd(), WM_USER, MBC_Window_GetHandle, 0));
}

MBError MusicBeeIPC::window_close() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Window_Close, 0));
}

MBError MusicBeeIPC::window_restore() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Window_Restore, 0));
}

MBError MusicBeeIPC::window_minimize() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Window_Minimize, 0));
}

MBError MusicBeeIPC::window_maximize() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Window_Maximize, 0));
}

MBError MusicBeeIPC::window_move(int x, int y) const
{
    COPYDATASTRUCT cds = pack(x, y);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Window_Move,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::window_resize(int w, int h) const
{
    COPYDATASTRUCT cds = pack(w, h);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_Window_Resize,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

MBError MusicBeeIPC::window_bringToFront() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Window_BringToFront, 0));
}

MBError MusicBeeIPC::window_getPosition(int& x, int& y) const
{
    MBError r = MBE_Error;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Window_GetPosition, 0);

    if (unpack(lr, x, y))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    return r;
}

MBError MusicBeeIPC::window_getSize(int& w, int& h) const
{
    MBError r = MBE_Error;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_Window_GetSize, 0);

    if (unpack(lr, w, h))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);
    
    return r;
}

MBMusicBeeVersion MusicBeeIPC::getMusicBeeVersion() const
{
    return static_cast<MBMusicBeeVersion>(SendMessage(findHwnd(), WM_USER, MBC_MusicBeeVersion, 0));
}

std::wstring MusicBeeIPC::getMusicBeeVersionStr() const
{
    switch (static_cast<MBMusicBeeVersion>(SendMessage(findHwnd(), WM_USER, MBC_MusicBeeVersion, 0)))
    {
        case MBMBV_v2_0:
            return L"2.0";
        case MBMBV_v2_1:
            return L"2.1";
        case MBMBV_v2_2:
            return L"2.2";
        case MBMBV_v2_3:
            return L"2.3";
        default:
            return L"Unknown";
    }
}

std::wstring MusicBeeIPC::getPluginVersionStr() const
{
    std::wstring r;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_PluginVersion, 0);

    unpack(lr, r);

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    return r;
}

MBError MusicBeeIPC::getPluginVersion(int& major, int& minor) const
{
    MBError r = MBE_Error;
    std::wstring v;

    HWND hwnd = findHwnd();

    LRESULT lr = SendMessage(hwnd, WM_USER, MBC_PluginVersion, 0);

    if (unpack(lr, v))
        r = MBE_NoError;

    SendMessage(hwnd, WM_USER, MBC_FreeLRESULT, lr);

    if (r == MBE_NoError)
    {
        std::wstringstream ss(v);

        std::wstring s_major;
        std::wstring s_minor;

        std::getline(ss, s_major, L'.');
        std::getline(ss, s_minor, L'.');

        ss = std::wstringstream(s_major);
        ss>>major;
        ss = std::wstringstream(s_minor);
        ss>>minor;
    }

    return r;
}

MBError MusicBeeIPC::test() const
{
    return static_cast<MBError>(SendMessage(findHwnd(), WM_USER, MBC_Test, 0));
}

MBError MusicBeeIPC::messageBox(const std::wstring& text, const std::wstring& caption) const
{
    COPYDATASTRUCT cds = pack(text, caption);

    MBError r = static_cast<MBError>(SendMessage(findHwnd(), WM_COPYDATA, MBC_MessageBox,
                                                 reinterpret_cast<LPARAM>(&cds)));

    free(cds);

    return r;
}

LPARAM MusicBeeIPC::toLPARAM(bool b) const
{
    return b ? (LPARAM)MB_True : (LPARAM)MB_False;
}

bool MusicBeeIPC::toBool(LRESULT r) const
{
    return r != MB_False;
}
