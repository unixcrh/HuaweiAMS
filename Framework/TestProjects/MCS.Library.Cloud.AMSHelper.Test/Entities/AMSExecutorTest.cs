﻿using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Data.Executors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    [TestClass]
    public class AMSExecutorTest
    {
        [TestMethod]
        public void UpdateEventExecutor()
        {
            AMSEventSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            AMSEditEntityExecutor<AMSEvent> executor = new AMSEditEntityExecutor<AMSEvent>(eventData,
                data => AMSEventSqlAdapter.Instance.Update(data), AMSOperationType.EditEvent);

            executor.Execute();

            AMSEvent eventLoaded = AMSEventSqlAdapter.Instance.LoadByChannelID(channel.ID).SingleOrDefault();

            Assert.IsNotNull(eventLoaded);
            eventData.AreEqual(eventLoaded);
        }

        [TestMethod]
        public void DeleteEventExecutor()
        {
            AMSEventSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            AMSEditEntityExecutor<AMSEvent> executor = new AMSEditEntityExecutor<AMSEvent>(eventData,
                data => AMSEventSqlAdapter.Instance.Update(data), AMSOperationType.EditEvent);

            executor.Execute();

            AMSEvent eventLoaded = AMSEventSqlAdapter.Instance.LoadByChannelID(channel.ID).SingleOrDefault();

            Assert.IsNotNull(eventLoaded);

            AMSVDeleteEntityExecutor<string, AMSEvent> deleteExecutor = new AMSVDeleteEntityExecutor<string, AMSEvent>(eventData.ID,
                key => AMSEventSqlAdapter.Instance.Delete(builder => builder.AppendItem("ID", key)), AMSOperationType.DeleteEvent);

            deleteExecutor.Execute();

            Assert.IsFalse(AMSEventSqlAdapter.Instance.Exists(builder => builder.AppendItem("ID", eventData.ID)));
        }
    }
}
