﻿using System;

namespace Konata.Msf.Packets.Tlv
{
    public class T204Body : TlvBody
    {
        public readonly string _url;

        public T204Body(string url)
            : base()
        {
            _url = url;

            PutString(_url);
        }

        public T204Body(byte[] data)
            : base(data)
        {
            TakeString(out _url, Prefix.None);
        }
    }
}
