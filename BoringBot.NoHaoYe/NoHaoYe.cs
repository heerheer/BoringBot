using Amiable.SDK;
using Amiable.SDK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoringBot.NoHaoYe
{
    public static class NoHaoYe
    {
        public static AppInfo GetAppInfo()
       => new AppInfo
       {
           Name = "BoringBot.NoHaoYe",
           Author = "Heer Kaisair",
           Version = "1.0.0",
           Description = "BoringBot·禁止好耶",
           AppId = "top.heerdev.boringbot.nohaoye"
       };
    }

}
