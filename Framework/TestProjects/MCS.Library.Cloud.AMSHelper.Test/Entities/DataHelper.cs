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
            data.State = AMSEventState.NotStart;
            data.StartTime = DateTime.Now.AddDays(1);
            data.EndTime = DateTime.Now.AddDays(2);

            return data;
        }

        public static UserOperationLog PrepareUserOperationLog(string resourceID)
        {
            UserOperationLog log = new UserOperationLog();

            log.ResourceID = resourceID;
            log.Subject = "测试日志";

            return log;
        }

        public static AMSUserView PrepareUserView(string eventID)
        {
            AMSUserView userView = new AMSUserView();

            userView.UserID = UuidHelper.NewUuidString();
            userView.UserName = userView.UserID;
            userView.EventID = eventID;
            userView.LastClientAccessIP = "127.0.0.1";

            return userView;
        }
    }
}
