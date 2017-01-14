//--------------------------------------------------------//
// MusicBeeIPCSDK C++ v2.0.0                              //
// Copyright © Kerli Low 2014                             //
// This file is licensed under the                        //
// BSD 2-Clause License                                   //
// See LICENSE_MusicBeeIPCSDK for more information.       //
//--------------------------------------------------------//

#include "MusicBeeIPC/MusicBeeIPC.h"


// --------------------------------------------------------------------------------
// All strings are encoded in UTF-16 little endian
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// Free lr after use
// --------------------------------------------------------------------------------
bool MusicBeeIPC::unpack(LRESULT lr, std::wstring& string_1) const
{
    string_1 = L"";

    void* mmf = OpenMmf(lr);
    if (mmf == NULL)
        return false;

    void* ptr = nullptr;
    void* view = MapMmfView(mmf, lr, ptr);
    if (view == nullptr)
    {
        CloseMmf(mmf);
        return false;
    }
    
    const char* cptr = static_cast<const char*>(ptr);

    int byteCount = 0;

    memcpy(&byteCount, cptr, sizeof(int));
    cptr += sizeof(int);

    if (byteCount > 0)
    {
        char* bytes = new char[byteCount + sizeof(wchar_t)];
        if (bytes == nullptr)
        {
            UnmapMmfView(view);
            CloseMmf(mmf);
            return false;
        }

        memset(bytes, 0, byteCount + sizeof(wchar_t));

        memcpy(bytes, cptr, byteCount);

        string_1 = reinterpret_cast<wchar_t*>(bytes);

        delete[] bytes;
    }
    
    UnmapMmfView(view);

    CloseMmf(mmf);

    return true;
}

// --------------------------------------------------------------------------------
// -Int32:  Number of strings
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Byte count of string
// -byte[]: String data
// -...
// Free lr after use
// --------------------------------------------------------------------------------
bool MusicBeeIPC::unpack(LRESULT lr, std::vector<std::wstring>& strings) const
{
    strings = std::vector<std::wstring>();
    
    void* mmf = OpenMmf(lr);
    if (mmf == nullptr)
        return false;
    
    void* ptr = nullptr;
    void* view = MapMmfView(mmf, lr, ptr);
    if (view == nullptr)
    {
        CloseMmf(mmf);
        return false;
    }
    
    const char* cptr = static_cast<const char*>(ptr);

    int strCount = 0;
    
    memcpy(&strCount, cptr, sizeof(int));
    cptr += sizeof(int);

    strings.resize(strCount);
    
    int byteCount = 0;

    static const size_t sowct = sizeof(wchar_t);

    for (auto& s : strings)
    {
        memcpy(&byteCount, cptr, sizeof(int));
        cptr += sizeof(int);

        if (byteCount > 0)
        {
            char* bytes = new char[byteCount + sowct];
            if (bytes == nullptr)
            {
                UnmapMmfView(view);
                CloseMmf(mmf);
                return false;
            }

            memset(bytes, 0, byteCount + sowct);

            memcpy(bytes, cptr, byteCount);
            cptr += byteCount;

            s = reinterpret_cast<wchar_t*>(bytes);

            delete[] bytes;
        }
    }

    UnmapMmfView(view);

    CloseMmf(mmf);

    return true;
}

// --------------------------------------------------------------------------------
// -Int32: 32 bit integer
// -Int32: 32 bit integer
// Free lr after use
// --------------------------------------------------------------------------------
bool MusicBeeIPC::unpack(LRESULT lr, int& int32_1, int& int32_2) const
{
    int32_1 = int32_2 = 0;

    void* mmf = OpenMmf(lr);
    if (mmf == nullptr)
        return false;
    
    void* ptr = nullptr;
    void* view = MapMmfView(mmf, lr, ptr);
    if (view == nullptr)
    {
        CloseMmf(mmf);
        return false;
    }
    
    const char* cptr = static_cast<const char*>(ptr);

    memcpy(&int32_1, cptr, sizeof(int));
    cptr += sizeof(int);

    memcpy(&int32_2, cptr, sizeof(int));

    UnmapMmfView(view);

    CloseMmf(mmf);

    return true;
}

// --------------------------------------------------------------------------------
// -Int32: Number of integers
// -Int32: 32 bit integer
// -Int32: 32 bit integer
// -...
// Free lr after use
// --------------------------------------------------------------------------------
bool MusicBeeIPC::unpack(LRESULT lr, std::vector<int>& int32s) const
{
    int32s = std::vector<int>();
    
    void* mmf = OpenMmf(lr);
    if (mmf == nullptr)
        return false;
    
    void* ptr = nullptr;
    void* view = MapMmfView(mmf, lr, ptr);
    if (view == nullptr)
    {
        CloseMmf(mmf);
        return false;
    }
    
    const char* cptr = static_cast<const char*>(ptr);

    int int32Count = 0;
    
    memcpy(&int32Count, cptr, sizeof(int));
    cptr += sizeof(int);

    int32s.resize(int32Count);
    
    memcpy(&int32s[0], cptr, sizeof(int) * int32Count);

    UnmapMmfView(view);

    CloseMmf(mmf);

    return true;
}


HANDLE MusicBeeIPC::OpenMmf(LRESULT lr) const
{
    if (lr == 0)
        return NULL;

    MBLRUShort ls(lr);

    wchar_t buffer[32] = {0};

    swprintf(buffer, 31, L"mbipc_mmf_%d", ls.s.low);

    return OpenFileMappingW(FILE_MAP_READ, FALSE, buffer);
}

void MusicBeeIPC::CloseMmf(HANDLE mmf) const
{
    CloseHandle(mmf);
}

void* MusicBeeIPC::MapMmfView(HANDLE mmf, LRESULT lr, void*& ptr) const
{
    MBLRUShort ls(lr);

    void* view = MapViewOfFile(mmf, FILE_MAP_READ, 0, 0, 0);

    ptr = static_cast<char*>(view) + ls.s.high + sizeof(__int64);

    return view;
}

void MusicBeeIPC::UnmapMmfView(const void* view) const
{
    UnmapViewOfFile(view);
}
