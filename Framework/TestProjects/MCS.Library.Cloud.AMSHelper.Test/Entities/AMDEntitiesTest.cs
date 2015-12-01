using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    [TestClass]
    public class AMDEntitiesTest
    {
        [TestMethod]
        public void UpdateAMSChannel()
        {
            AMSChannelSqlAdapter.Instance.ClearAll();

            AMSChannel channel = PrepareData();
            AMSChannelSqlAdapter.Instance.Update(channel);

            AMSChannel channelLoaded = AMSChannelSqlAdapter.Instance.LoadByInBuilder(builder => builder.AppendItem(channel.ID), "ID").SingleOrDefault();

            Assert.IsNotNull(channelLoaded);
            AssertEqual(channel, channelLoaded);
        }

        private static void AssertEqual(AMSChannel expected, AMSChannel actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.State, actual.State);
        }

        private static AMSChannel PrepareData()
        {
            AMSChannel channel = new AMSChannel();

            channel.ID = UuidHelper.NewUuidString();
            channel.Name = "Test Channel";
            channel.Description = "Test Channel Description";
            channel.State = AMSChannelState.Running;

            return channel;
        }
    }
}
