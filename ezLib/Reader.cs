﻿using System;



namespace easyLib
{
    public interface IReader
    {
        int Read(byte[] buffer, int bufferOffset, int count);
        byte[] ReadBytes(int byteCount);
        sbyte ReadSByte();
        byte ReadByte();
        bool ReadBoolean();
        char ReadChar();
        short ReadShort();
        ushort ReadUShort();
        int ReadInt();
        uint ReadUInt();
        long ReadLong();
        ulong ReadULong();
        float ReadFloat();
        double ReadDouble();
        decimal ReadDecimal();
        string ReadString();
        DateTime ReadTime();
        void Skip(int byteCount);
    }    
}
