﻿using System;
using Konata.Msf.Packets.Oidb;

namespace Konata.Msf.Services.OidbSvc
{
    public class Oidb0x55c_1 : Service
    {
        private Oidb0x55c_1()
        {
            Register("OidbSvc.0x55c_1", this);
        }

        public static Service Instance { get; } = new Oidb0x55c_1();

        public override bool OnRun(Core core, string method, params object[] args)
        {
            if (method != "")
                return false;

            if (args.Length != 3)
                return false;

            if (args[0] is uint groupUin
                && args[1] is uint memberUin
                && args[2] is bool promoteAdmin)
                return Request_0x55c_1(core, groupUin, memberUin, promoteAdmin);

            return false;
        }

        public override bool OnHandle(Core core, params object[] args)
        {
            return false;
        }

        private bool Request_0x55c_1(Core core, uint groupUin,
            uint memberUin, bool promoteAdmin)
        {
            var oidbPacket = new OidbCmd0x55c_1(groupUin, memberUin, promoteAdmin);
            core.SsoMan.PostMessage(this, oidbPacket);

            return true;
        }
    }
}
