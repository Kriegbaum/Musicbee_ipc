//--------------------------------------------------------//
// MusicBeeIPCSDK C++ v2.0.0                              //
// Copyright © Kerli Low 2014                             //
// This file is licensed under the                        //
// BSD 2-Clause License                                   //
// See LICENSE_MusicBeeIPCSDK for more information.       //
//--------------------------------------------------------//

#ifndef MUSICBEEIPC_STRUCTS_H
#define MUSICBEEIPC_STRUCTS_H


union MBFloatInt
{
    float f;
    int i;

    MBFloatInt() : i(0) { }
    MBFloatInt(float f) : f(f) { }
    MBFloatInt(int i) : i(i) { }
};

union MBLRUShort
{
    LRESULT lr;

    struct
    {
        unsigned short low;
        unsigned short high;
    } s;

    MBLRUShort() : lr(0) { }
    MBLRUShort(LRESULT lr) : lr(lr) { }
    MBLRUShort(unsigned short low, unsigned short high) { s.low = low; s.high = high; }
};



#endif // MUSICBEEIPC_STRUCTS_H
