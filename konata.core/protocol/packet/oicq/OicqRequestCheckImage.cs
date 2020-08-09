﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konata.Protocol.Packet.Oicq
{
    public class OicqRequestCheckImage : OicqRequest
    {
        public OicqRequestCheckImage()
        {
            _cmd = 0x0810;
            _subCmd = 0x02;
            _serviceCmd = "wtlogin.login";
        }

    }
}