﻿using System;

namespace Konata.Msf.Packets.Tlv
{
    public class T145Body : TlvBody
    {
        public readonly byte[] _guid;

        public T145Body(byte[] guid, int guidLength)
            : base()
        {
            _guid = guid;

            PutBytes(_guid);
        }

        public T145Body(byte[] data)
            : base(data)
        {
            TakeBytes(out _guid, Prefix.None);
        }
    }
}
