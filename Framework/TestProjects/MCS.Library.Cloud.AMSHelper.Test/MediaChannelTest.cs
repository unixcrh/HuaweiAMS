﻿using MCS.Library.Cloud.AMS.DataObjects;
using MCS.Library.Cloud.AMSHelper.Configuration;
using MCS.Library.Cloud.AMSHelper.Mechanism;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;

namespace MCS.Library.Cloud.AMSHelper.Test
{
    [TestClass]
    public class MediaChannelTest
    {
        [TestMethod]
        public void ListChannels()
        {
            CloudMediaContext context = MediaServiceAccountSettings.GetConfig().Accounts.GetCloudMediaContext("eastAsia");

            AMSChannelCollection channels = context.Channels.ToAMSChannels();

            foreach (AMSChannel channel in channels)
            {
                Console.WriteLine("{0}-{1}-{2}", channel.ID, channel.Name, channel.LastModified);
            }
        }
    }
}
