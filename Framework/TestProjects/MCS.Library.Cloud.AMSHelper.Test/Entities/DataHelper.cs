using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    internal static class DataHelper
    {
        public static AMSChannel PrepareChannelData()
        {
            AMSChannel channel = new AMSChannel();

            channel.ID = UuidHelper.NewUuidString();
            channel.Name = "Test Channel";
            channel.Description = "Test Channel Description";
            channel.State = AMSChannelState.Running;

            return channel;
        }

        public static AMSEvent PrepareEventData(string channelID)
        {
            AMSEvent data = new AMSEvent();

            data.ID = UuidHelper.NewUuidString();
            data.Name = "Test Event";
            data.ChannelID = channelID;
            data.Description = "Test Event Description";
            data.State = AMSEventState.Running;
            data.StartTime = DateTime.UtcNow.AddDays(1);
            data.EndTime = DateTime.UtcNow.AddDays(2);

            return data;
        }

        public static UserOperationLog PrepareUserOperationLog(string resourceID)
        {
            UserOperationLog log = new UserOperationLog();

            log.ResourceID = resourceID;
            log.Subject = "测试日志";

            return log;
        }
    }
}
