﻿using System;
using Konata.Msf;
using Konata.Debug.DevToolsProtocol;
using System.Collections.Generic;

namespace Konata.Debug
{
    internal class Program
    {
        static Bot bot;

        public static void Main()
        {
            bot = new Bot(3322047216, "0hrwcupn5");
            bot.RegisterDelegate(EventProc);
            bot.Run();

            Console.Write("Exit.");
        }

        private static bool EventProc(EventType type, params object[] args)
        {
            switch (type)
            {
                case EventType.BotStart:
                    return OnBootstrap();
                case EventType.WtLoginVerifySliderCaptcha:
                    return OnSliderCaptchaRemote((string)args[0]);
                case EventType.WtLoginVerifySmsCaptcha:
                    return OnSmsCaptcha((string)args[0]);
                case EventType.WtLoginVerifyImageCaptcha:
                    return OnImageCaptcha();
                case EventType.GroupMessage:
                    return OnGroupMessage();
                case EventType.PrivateMessage:
                    return OnPrivateMessage();
            }

            return false;
        }

        /// <summary>
        /// 框架啓動事件
        /// </summary>
        /// <returns></returns>
        private static bool OnBootstrap()
        {
            bot.Login();
            return true;
        }

        /// <summary>
        /// 驗證滑塊驗證碼
        /// </summary>
        /// <param name="captchaURL"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        private static bool OnSliderCaptcha(string captchaURL, string userAgent)
        {
            Console.WriteLine($"  CaptchaUrl => {captchaURL}");

            var browser = new Browser();
            Console.WriteLine($"Opening browser...");

            if (!browser.Open(Browser.Chrome, userAgent, 504, 896, true))
            {
                Console.WriteLine($"Your device haven't installed chromium kernel family web browser. (Konata slider needs DevTools Protocol support.)");
                return false;
            }

            string ticket;
            var session = browser.ConnectToFirstTab();
            {
                session.EnableNetwork(65536);
                session.EnableTouchSimulation(1, "mobile");
                session.SetCookie("uin", "", ".captcha.qq.com");
                session.SetCookie("qq_locale_id", "2052", ".captcha.qq.com");
                session.SetCookie("login_key_set_failed", "AlreadyLogout", ".captcha.qq.com");
                session.SetExtraHeaders(new Dictionary<string, string>
                {
                    ["sec-ch-ua"] = "",
                    ["Sec-Fetch-Dest"] = "",
                    ["sec-ch-ua-mobile"] = "",
                    ["X-Requested-With"] = "com.tencent.mobileqq",
                    ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3",
                    ["Accept-Language"] = "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7",
                });

                session.NavigateTo(captchaURL);
                ticket = session.WaitForTicket();
            }
            session.Close();
            browser.WaitForExit();

            if (ticket == null || ticket == "")
            {
                return false;
            }

            // 提交驗證
            Console.WriteLine($"Ticket got => \n{ticket}");
            bot.SubmitSliderTicket(ticket);

            return true;
        }

        /// <summary>
        /// 驗證滑塊驗證碼. 手動輸入
        /// </summary>
        /// <param name="captchaURL"></param>
        /// <returns></returns>
        private static bool OnSliderCaptchaRemote(string captchaURL)
        {
            Console.WriteLine($"  CaptchaUrl => {captchaURL}");
            Console.WriteLine("Please paste the ticket: ");

            var ticket = Console.ReadLine();

            bot.SubmitSliderTicket(ticket);
            return true;
        }

        /// <summary>
        /// 驗證短訊驗證碼
        /// </summary>
        /// <param name="sigPhone"></param>
        /// <returns></returns>
        private static bool OnSmsCaptcha(string sigPhone)
        {
            Console.Write($"We sent an SMS to your phone number {sigPhone}, Please type the code you've received: ");
            var sigSmsCode = Console.ReadLine();

            Console.WriteLine($"SMS Code got => \n{sigSmsCode}");
            bot.SubmitSmsCode(sigSmsCode);

            return false;
        }

        private static bool OnImageCaptcha()
        {
            return false;
        }

        private static bool OnPrivateMessage()
        {
            return false;
        }

        private static bool OnGroupMessage()
        {
            return false;
        }
    }
}
