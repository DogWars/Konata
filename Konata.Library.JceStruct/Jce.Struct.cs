﻿using System;
using System.Collections.Generic;

namespace Konata.Library.JceStruct
{
    public static partial class Jce
    {
        public sealed class Struct : SortedDictionary<byte, IObject>, IIndexable
        {
            public Type Type
            {
                get
                {
                    return Type.StructBegin;
                }
            }

            public IObject this[string path]
            {
                get
                {
                    int dot = path.IndexOf('.');
                    if (dot >= 0)
                    {
                        byte tag = byte.Parse(path.Substring(0, dot));
                        if (ContainsKey(tag))
                        {
                            if (this[tag] is IIndexable ind)
                            {
                                return ind[path.Substring(dot + 1)];
                            }
                            else
                            {
                                throw new InvalidCastException();
                            }
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                    else
                    {
                        byte tag = byte.Parse(path);
                        if (ContainsKey(tag))
                        {
                            return this[tag];
                        }
                        else
                        {
                            throw new KeyNotFoundException();
                        }
                    }
                }
            }

            public Struct() : base() { }

            public Struct(IDictionary<byte, IObject> data) : base(data) { }

            public static implicit operator byte[](Struct data)
            {
                return Serialize(data);
            }

            public static implicit operator Struct(byte[] data)
            {
                return Deserialize(data);
            }
        }
    }
}