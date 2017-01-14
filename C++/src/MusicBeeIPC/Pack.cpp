//--------------------------------------------------------//
// MusicBeeIPCSDK C++ v2.0.0                              //
// Copyright © Kerli Low 2014                             //
// This file is licensed under the                        //
// BSD 2-Clause License                                   //
// See LICENSE_MusicBeeIPCSDK for more information.       //
//--------------------------------------------------------//

#include "MusicBeeIPC/MusicBeeIPC.h"


void MusicBeeIPC::free(COPYDATASTRUCT& cds) const
{
    if (cds.lpData != nullptr)
    {
        delete[] cds.lpData;
        cds.lpData = nullptr;
    }
}

// --------------------------------------------------------------------------------
// All strings are encoded in UTF-16 little endian
// --------------------------------------------------------------------------------

// --------------------------------------------------------------------------------
// -Int32: 32 bit integer
// -Int32: 32 bit integer
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(int int32_1, int int32_2) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    cds.cbData = sizeof(int) * 2;

    cds.lpData = new char[cds.cbData];
    
    char* cptr = static_cast<char*>(cds.lpData);

    memcpy(cptr, &int32_1, sizeof(int));
    cptr += sizeof(int);
    memcpy(cptr, &int32_2, sizeof(int));

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;
    
    int byteCount = string_1.size() * sizeof(wchar_t);

    cds.cbData = sizeof(int) + byteCount;

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Byte count of string
// -byte[]: String data
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, const std::wstring& string_2) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;
    
    cds.cbData = sizeof(int) * 2 + sizeof(wchar_t) * (string_1.size() + string_2.size());

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);
    
    int byteCount = string_1.size() * sizeof(wchar_t);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;

    byteCount = string_2.size() * sizeof(wchar_t);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_2[0], byteCount);

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Byte count of string
// -byte[]: String data
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, const std::wstring& string_2,
                                 const std::wstring& string_3) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;
    
    cds.cbData = sizeof(int) * 3 + sizeof(wchar_t) * (string_1.size() + string_2.size() + string_3.size());

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);
    
    int byteCount = string_1.size() * sizeof(wchar_t);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;

    byteCount = string_2.size() * sizeof(wchar_t);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_2[0], byteCount);
    cptr += byteCount;

    byteCount = string_3.size() * sizeof(wchar_t);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_3[0], byteCount);

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  32 bit integer
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, int int32_1) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    int byteCount = string_1.size() * sizeof(wchar_t);

    cds.cbData = sizeof(int) * 2 + byteCount;

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;

    memcpy(cptr, &int32_1, sizeof(int));

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  32 bit integer
// -Int32:  32 bit integer
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, int int32_1, int int32_2) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    int byteCount = string_1.size() * sizeof(wchar_t);

    cds.cbData = sizeof(int) * 3 + byteCount;

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;

    memcpy(cptr, &int32_1, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &int32_2, sizeof(int));

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  bool
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, bool bool_1) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    int byteCount = string_1.size() * sizeof(wchar_t);

    cds.cbData = sizeof(int) * 2 + byteCount;

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;
    
    int i = bool_1 ? MB_True : MB_False;
    memcpy(cptr, &i, sizeof(int));

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -double: 64-bit floating-point value
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, double double_1) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    int byteCount = string_1.size() * sizeof(wchar_t);

    cds.cbData = sizeof(int) + byteCount + sizeof(double);

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);

    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;

    memcpy(cptr, &double_1, sizeof(double));

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Number of strings in string array
// -Int32:  Byte count of string in string array
// -byte[]: String data in string array
// -Int32:  Byte count of string in string array
// -byte[]: String data in string array
// -...
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, const std::vector<std::wstring>& strings) const   
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    cds.cbData = string_1.size();

    for (const auto& s : strings)
        cds.cbData += s.size();

    cds.cbData *= sizeof(wchar_t);

    cds.cbData += sizeof(int) * (strings.size() + 2);

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);
    
    int byteCount = string_1.size() * sizeof(wchar_t);
    
    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;

    int strCount = strings.size();
    memcpy(cptr, &strCount, sizeof(int));
    cptr += sizeof(int);
    
    for (const auto& s : strings)
    {
        byteCount = s.size() * sizeof(wchar_t);

        memcpy(cptr, &byteCount, sizeof(int));
        cptr += sizeof(int);

        memcpy(cptr, &s[0], byteCount);
        cptr += byteCount;
    }

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Number of strings in string array
// -Int32:  Byte count of string in string array
// -byte[]: String data in string array
// -Int32:  Byte count of string in string array
// -byte[]: String data in string array
// -...
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, const std::wstring& string_2,
                                 const std::vector<std::wstring>& strings) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    cds.cbData = string_1.size() + string_2.size();

    for (const auto& s : strings)
        cds.cbData += s.size();

    cds.cbData *= sizeof(wchar_t);

    cds.cbData += sizeof(int) * (strings.size() + 3);

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);
    
    int byteCount = string_1.size() * sizeof(wchar_t);
    
    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;

    byteCount = string_2.size() * sizeof(wchar_t);
    
    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_2[0], byteCount);
    cptr += byteCount;

    int strCount = strings.size();
    memcpy(cptr, &strCount, sizeof(int));
    cptr += sizeof(int);
    
    for (const auto& s : strings)
    {
        byteCount = s.size() * sizeof(wchar_t);

        memcpy(cptr, &byteCount, sizeof(int));
        cptr += sizeof(int);

        memcpy(cptr, &s[0], byteCount);
        cptr += byteCount;
    }

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  32 bit integer
// -Int32:  Byte count of string
// -byte[]: String data
// -...
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, int int32_1, const std::wstring& string_2) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;
    
    cds.cbData = sizeof(int) * 3 + sizeof(wchar_t) * (string_1.size() + string_2.size());

    cds.lpData = new char[cds.cbData];

    char* cptr = static_cast<char*>(cds.lpData);
    
    int byteCount = string_1.size() * sizeof(wchar_t);
    
    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;
    
    memcpy(cptr, &int32_1, sizeof(int));
    cptr += sizeof(int);

    byteCount = string_2.size() * sizeof(wchar_t);
    
    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_2[0], byteCount);
    cptr += byteCount;

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32: Number of integers in integer array
// -Int32: 32 bit integer
// -Int32: 32 bit integer
// -...
// -Int32: 32 bit integer
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::vector<int>& int32s, int int32_1) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;

    cds.cbData = sizeof(int) * (int32s.size() + 2);

    cds.lpData = new char[cds.cbData];
    
    char* cptr = static_cast<char*>(cds.lpData);
    
    memcpy(cptr, &int32s[0], int32s.size() * sizeof(int));
    cptr += int32s.size() * sizeof(int);

    memcpy(cptr, &int32_1, sizeof(int));

    return cds;
}

// --------------------------------------------------------------------------------
// -Int32:  Byte count of string
// -byte[]: String data
// -Int32:  Number of integers in integer array
// -Int32:  32 bit integer
// -Int32:  32 bit integer
// -...
// -Int32:  32 bit integer
// Free the returned COPYDATASTRUCT after use
// --------------------------------------------------------------------------------
COPYDATASTRUCT MusicBeeIPC::pack(const std::wstring& string_1, const std::vector<int>& int32s, int int32_1) const
{
    COPYDATASTRUCT cds;
    cds.dwData = 0;
    
    int byteCount = string_1.size() * sizeof(wchar_t);
    
    cds.cbData = sizeof(int) * (int32s.size() + 3) + byteCount;

    cds.lpData = new char[cds.cbData];
    
    char* cptr = static_cast<char*>(cds.lpData);
    
    memcpy(cptr, &byteCount, sizeof(int));
    cptr += sizeof(int);

    memcpy(cptr, &string_1[0], byteCount);
    cptr += byteCount;
    
    memcpy(cptr, &int32s[0], int32s.size() * sizeof(int));
    cptr += int32s.size() * sizeof(int);

    memcpy(cptr, &int32_1, sizeof(int));

    return cds;
}
