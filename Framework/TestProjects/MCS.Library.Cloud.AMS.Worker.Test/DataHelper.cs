using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Test
{
    public static class DataHelper
    {
        private const string TestChannelName = "TheFirst";

        public static void ClearAllEvents()
        {
            AMSEventSqlAdapter.Instance.ClearAll();
        }

        public static void AddEvent()
        {
            AMSEvent eventData = new AMSEvent();

            eventData.ID = UuidHelper.NewUuidString();

            eventData.Name = string.Format("新建节目{0:yyyy-MM-dd HH:mm:ss.fff}", DateTime.UtcNow);
            eventData.State = AMSEventState.NotStart;
            eventData.StartTime = DateTime.UtcNow.AddMinutes(5);
            eventData.EndTime = DateTime.UtcNow.AddMinutes(35);

            AMSChannel channel = AMSChannelSqlAdapter.Instance.Load(builder => builder.AppendItem("Name", TestChannelName)).SingleOrDefault();
            eventData.ChannelID = channel.ID;

            AMSEventSqlAdapter.Instance.Update(eventData);
        }

        public static void ClearQueue()
        {
            AMSQueueSqlAdapter.Instance.ClearQueue();
        }
    }
}
