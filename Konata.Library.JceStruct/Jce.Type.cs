﻿namespace Konata.Library.JceStruct
{
    public static partial class Jce
    {
        public enum Type : byte
        {
            Byte = 0,
            Short = 1,
            Int = 2,
            Long = 3,
            Float = 4,
            Double = 5,
            String1 = 6,
            String4 = 7,
            Map = 8,
            List = 9,
            StructBegin = 10,
            StructEnd = 11,
            ZeroTag = 12,
            SimpleList = 13,
            None = 255
        }
    }
}