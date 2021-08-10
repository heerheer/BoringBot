using Amiable.Adapter.Kum;
using Amiable.Adapter.MQ;
using Amiable.SDK;
using Amiable.SDK.Interface;
using BoringBot.NoHaoYe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Amiable.Core
{
    public static class AmiableService
    {
        public static AppService App;

        public static List<IPluginEvent> Events = new List<IPluginEvent>();

        //会在Init后注册。
        public static void RegEvents()
        {
            //注册事件
            //这里很有必要因为Assembly读不了...我也不知道怎么回事
            //Events.Add((IPluginEvent)Activator.CreateInstance<>()));

            //上面那个是.net core开发时候的问题...
            var ass = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var item in ass)
            {
                var types = item.GetTypes().ToList().Where(s => s != typeof(IPluginEvent) && typeof(IPluginEvent).IsAssignableFrom(s));
                types.ToList().ForEach(
                    t =>
                    {
                        if (!Events.Exists(x => x.GetType() == t))
                        {
                            Events.Add((IPluginEvent)Activator.CreateInstance(t));
                            AmiableService.App.Log($"[注册事件]实例类型:{t.Name}");
                        }
                    }
                    );
            }
        }

        static AmiableService()
        {
            
            //初始化
            App = new AppService();
            SetAppInfo();
            ServiceBuilder(App);
            RegEvents();//注册所有事件
            App.Log("[AppDomain]",AppDomain.CurrentDomain.FriendlyName);
        }

        /// <summary>
        /// 设置App信息
        /// </summary>
        public static void SetAppInfo()
        {
            App.AppInfo = NoHaoYe.GetAppInfo();
        }

        /// <summary>
        /// 对AppService的建造
        /// </summary>
        /// <param name="service"></param>
        public static void ServiceBuilder(AppService service)
        {

            service.UseMQConfig();
        }
    }
}
