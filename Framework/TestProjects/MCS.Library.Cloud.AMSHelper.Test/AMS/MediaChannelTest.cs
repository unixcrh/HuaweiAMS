using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMSHelper.Configuration;
using MCS.Library.Cloud.AMSHelper.Mechanism;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;

namespace MCS.Library.Cloud.AMSHelper.Test.AMS
{
    [TestClass]
    public class MediaChannelTest
    {
        [TestMethod]
        public void ListChannelsFromConfig()
        {
            AMSChannelSqlAdapter.Instance.ClearAll();
            AMSChannelCollection channels = LiveChannelManager.GetAllChannels();

            Output(channels);
        }

        [TestMethod]
        public void UpdateAllChannelsToDB()
        {
            AMSChannelSqlAdapter.Instance.ClearAll();

            AMSChannelCollection channels = LiveChannelManager.GetAllChannels(true);

            AMSChannelSqlAdapter.Instance.UpdateAllChannels(channels);

            Output(channels);
        }

        [TestMethod]
        public void UpdateAllChannelsWithExistsData()
        {
            AMSChannelSqlAdapter.Instance.ClearAll();

            AMSChannelCollection channels = LiveChannelManager.GetAllChannels(true);

            AMSChannelSqlAdapter.Instance.UpdateAllChannels(channels);

            AMSChannelCollection channelsLoaded = LiveChannelManager.GetAllChannels(true);

            Output(channelsLoaded);

            Assert.AreEqual(channels.Count, channelsLoaded.Count);
        }

        private static void Output(IEnumerable<AMSChannel> channels)
        {
            foreach (AMSChannel channel in channels)
            {
                Console.WriteLine("ID: {0}", channel.ID);
                Console.WriteLine("AMSID: {0}", channel.AMSID);
                Console.WriteLine("Name: {0}", channel.Name);
                Console.WriteLine("LastModified: {0}", channel.AMSLastModified);
                Console.WriteLine("State: {0}", channel.State);

                Console.WriteLine();
            }
        }
    }
}
