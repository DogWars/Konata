﻿using System;

namespace Konata.Msf.Packets.Tlv
{
    public class T11aBody : TlvBody
    {
        public readonly byte _age;
        public readonly ushort _face;
        public readonly string _nickName;

        public T11aBody(ushort face, byte age, string nickName)
            : base()
        {
            _face = face;
            _age = age;
            _nickName = nickName;

            PutUshortBE(_face);
            PutByte(_age);
            PutString(_nickName, Prefix.Uint8);
        }

        public T11aBody(byte[] data)
            : base(data)
        {
            TakeUshortBE(out _face);
            TakeByte(out _age);
            TakeString(out _nickName, Prefix.Uint8);
        }
    }
}
