﻿using System;

namespace Konata.Msf.Packets.Oidb
{
    /// <summary>
    /// 設置群成員頭銜
    /// </summary>

    public class OidbCmd0x8fc_2 : OidbCmd0x8fc
    {
        public OidbCmd0x8fc_2(uint groupUin, uint memberUin,
            string specitalTitle, int? expiredTime = null)

            : base(0x02, new ReqBody
            {
                group_code = groupUin,
                rpt_mem_level_info = new MemberInfo
                {
                    uin = memberUin,
                    uin_name = specitalTitle,
                    special_title = specitalTitle,
                    special_title_expire_time = expiredTime ?? -1
                }
            })
        {

        }
    }
}
