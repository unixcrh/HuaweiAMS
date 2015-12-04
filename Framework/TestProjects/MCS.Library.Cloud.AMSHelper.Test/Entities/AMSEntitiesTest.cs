﻿using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    [TestClass]
    public class AMSEntitiesTest
    {
        [TestMethod]
        public void UpdateAMSChannel()
        {
            AMSChannelSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();
            AMSChannelSqlAdapter.Instance.Update(channel);

            AMSChannel channelLoaded = AMSChannelSqlAdapter.Instance.LoadByInBuilder(builder => builder.AppendItem(channel.ID), "ID").SingleOrDefault();

            Assert.IsNotNull(channelLoaded);
            AssertEqual(channel, channelLoaded);
        }

        [TestMethod]
        public void UpdateAMSEvent()
        {
            AMSEventSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            AMSEventSqlAdapter.Instance.Update(eventData);
            AMSEvent eventLoaded = AMSEventSqlAdapter.Instance.LoadByChannelID(channel.ID).SingleOrDefault();

            Assert.IsNotNull(eventLoaded);
            AssertEqual(eventData, eventLoaded);
        }

        [TestMethod]
        public void AddUserOperationLog()
        {
            string resourceID = UuidHelper.NewUuidString();

            UserOperationLog log = DataHelper.PrepareUserOperationLog(resourceID);

            long logID = UserOperationLogSqlAdapter.Instance.Add(log);
            UserOperationLog logLoaded = UserOperationLogSqlAdapter.Instance.LoadByID(logID);

            Assert.IsNotNull(logLoaded);
            AssertEqual(log, logLoaded);

            logLoaded = UserOperationLogSqlAdapter.Instance.LoadByResourceID(log.ResourceID).SingleOrDefault();

            Assert.IsNotNull(logLoaded);
            AssertEqual(log, logLoaded);
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

        private static void AssertEqual(AMSEvent expected, AMSEvent actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.ChannelID, actual.ChannelID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.State, actual.State);
        }

        private static void AssertEqual(UserOperationLog expected, UserOperationLog actual)
        {
            Assert.AreEqual(expected.ResourceID, actual.ResourceID);
            Assert.AreEqual(expected.Subject, actual.Subject);
        }
    }
}
