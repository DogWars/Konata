﻿using System;
using Konata.Library.IO;

namespace Konata.Msf.Packets.Oidb
{
    /// <summary>
    /// 群禁言
    /// </summary>

    public class OidbCmd0x570_8 : OidbSSOPkg
    {
        public OidbCmd0x570_8(uint groupUin, uint memberUin, uint timeSeconds)

            : base(0x570, 0x08, 0x00, (ByteBuffer root) =>
             {
                 root.PutUintBE(groupUin);
                 root.PutHexString("20 00 01");
                 root.PutUintBE(memberUin);
                 root.PutUintBE(timeSeconds);
             })
        {

        }
    }
}
