using Amiable.SDK.Enum;
using Amiable.SDK.EventArgs;
using Amiable.SDK.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoringBot.NoHaoYe.CustomEvents
{
    class NoHaoYeGroupEvent : IPluginEvent
    {
        public AmiableEventType EventType => AmiableEventType.Group;

        private Dictionary<long, DateTime> Datas = new Dictionary<long, DateTime>();

        public void Process(AmiableEventArgs _e)
        {

            var e = (AmiableMessageEventArgs)_e;
            if (!e.RawMessage.Contains("好耶"))
                return;


            if (!Datas.ContainsKey(e.GroupId))
            {
                //如果没有上次的数据就说明这次可以直接发，然后设置这次时间。
                e.SendMessage("禁止好耶");
                Datas.Add(e.GroupId, DateTime.Now);
            }
            else
            {
                //如果有上次的数据就说明这次需要判断下时间，然后设置这次时间。

                if ((DateTime.Now - Datas[e.GroupId]) > TimeSpan.FromSeconds(10))
                {
                    //10ss冷却哦
                    e.SendMessage("禁止好耶");
                    Datas[e.GroupId] = DateTime.Now;
                }
            }
            e.HandleResult = EventHandleResult.INTERCEPT;
        }
    }
}
