using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Builder;
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
            channel.AreEqual(channelLoaded);
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
            eventData.AreEqual(eventLoaded);
        }

        [TestMethod]
        public void LoadNeedStartEventInTimeFrame()
        {
            AMSEventSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            eventData.StartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));

            AMSEventSqlAdapter.Instance.Update(eventData);

            AMSEventCollection eventsLoaded = AMSEventSqlAdapter.Instance.LoadNeedStartEvents(TimeSpan.FromMinutes(15));

            Assert.IsTrue(eventsLoaded.Count > 0);
        }

        [TestMethod]
        public void LoadNeedStartEventOutTimeFrame()
        {
            AMSEventSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            eventData.StartTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));

            AMSEventSqlAdapter.Instance.Update(eventData);

            AMSEventCollection eventsLoaded = AMSEventSqlAdapter.Instance.LoadNeedStartEvents(TimeSpan.FromMinutes(5));

            Assert.AreEqual(0, eventsLoaded.Count);
        }

        [TestMethod]
        public void LoadNeedStopEventInTimeFrame()
        {
            AMSEventSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            eventData.State = AMSEventState.Running;
            eventData.EndTime = DateTime.UtcNow.Add(-TimeSpan.FromMinutes(10));

            AMSEventSqlAdapter.Instance.Update(eventData);

            AMSEventCollection eventsLoaded = AMSEventSqlAdapter.Instance.LoadNeedStopEvents();

            Assert.IsTrue(eventsLoaded.Count > 0);
        }

        [TestMethod]
        public void LoadNeedStopEventOutTimeFrame()
        {
            AMSEventSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            eventData.State = AMSEventState.Running;
            eventData.EndTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(10));

            AMSEventSqlAdapter.Instance.Update(eventData);

            AMSEventCollection eventsLoaded = AMSEventSqlAdapter.Instance.LoadNeedStopEvents();

            Assert.AreEqual(0, eventsLoaded.Count);
        }

        [TestMethod]
        public void AddUserOperationLog()
        {
            string resourceID = UuidHelper.NewUuidString();

            UserOperationLog log = DataHelper.PrepareUserOperationLog(resourceID);

            long logID = UserOperationLogSqlAdapter.Instance.Add(log);
            UserOperationLog logLoaded = UserOperationLogSqlAdapter.Instance.LoadByID(logID);

            Assert.IsNotNull(logLoaded);
            log.AreEqual(logLoaded);

            logLoaded = UserOperationLogSqlAdapter.Instance.LoadByResourceID(log.ResourceID).SingleOrDefault();

            Assert.IsNotNull(logLoaded);
            log.AreEqual(logLoaded);
        }

        [TestMethod]
        public void UpdateUserView()
        {
            AMSEventSqlAdapter.Instance.ClearAll();
            AMSUserViewSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            AMSEventSqlAdapter.Instance.Update(eventData);

            AMSUserView userView = DataHelper.PrepareUserView(eventData.ID);

            AMSUserViewSqlAdapter.Instance.UpdateUserView(userView);

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("EventID", userView.EventID);
            builder.AppendItem("UserID", userView.UserID);

            AMSUserView userViewLoaded = AMSUserViewSqlAdapter.Instance.LoadByBuilder(builder).Single();

            AMSEvent eventLoaded = AMSEventSqlAdapter.Instance.LoadByID(eventData.ID);

            Assert.AreEqual(1, eventLoaded.Views);
        }

        [TestMethod]
        public void UpdateExistedUserView()
        {
            AMSEventSqlAdapter.Instance.ClearAll();
            AMSUserViewSqlAdapter.Instance.ClearAll();

            AMSChannel channel = DataHelper.PrepareChannelData();

            AMSEvent eventData = DataHelper.PrepareEventData(channel.ID);

            AMSEventSqlAdapter.Instance.Update(eventData);

            AMSUserView userView = DataHelper.PrepareUserView(eventData.ID);

            AMSUserViewSqlAdapter.Instance.UpdateUserView(userView);

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("EventID", userView.EventID);
            builder.AppendItem("UserID", userView.UserID);

            AMSUserView userViewLoaded = AMSUserViewSqlAdapter.Instance.LoadByBuilder(builder).Single();

            AMSEvent eventLoaded = AMSEventSqlAdapter.Instance.LoadByID(eventData.ID);

            Assert.AreEqual(1, eventLoaded.Views);

            //再保存一下
            AMSUserViewSqlAdapter.Instance.UpdateUserView(userView);

            eventLoaded = AMSEventSqlAdapter.Instance.LoadByID(eventData.ID);

            //用户观看次数依然是1
            Assert.AreEqual(1, eventLoaded.Views);
        }
    }
}
