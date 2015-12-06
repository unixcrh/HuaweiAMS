using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void AddMessages()
        {
            AMSQueueSqlAdapter.Instance.ClearQueue();

            IQueue<AMSQueueItem> queue = QueueManager.GetQueue<AMSQueueItem>("amsQueue");

            queue.AddMessages(string.Empty, CreateMessage(), CreateMessage());

            IEnumerable<AMSQueueItem> loaded = queue.GetMessages(string.Empty, 10);

            Assert.AreEqual(2, loaded.Count());

            loaded = queue.GetMessages(string.Empty, 10);
            Assert.AreEqual(0, loaded.Count());
        }

        [TestMethod]
        public void PeekMessages()
        {
            AMSQueueSqlAdapter.Instance.ClearQueue();

            IQueue<AMSQueueItem> queue = QueueManager.GetQueue<AMSQueueItem>("amsQueue");

            queue.AddMessages(string.Empty, CreateMessage(), CreateMessage());

            IEnumerable<AMSQueueItem> loaded = queue.PeekMessages(string.Empty, 10);

            Assert.AreEqual(2, loaded.Count());

            loaded = queue.PeekMessages(string.Empty, 10);
            Assert.AreEqual(2, loaded.Count());
        }

        private static IQueue<AMSQueueItem> GetQueue()
        {
            return QueueManager.GetQueue<AMSQueueItem>("amsQueue");
        }

        private static AMSQueueItem CreateMessage()
        {
            AMSQueueItem message = new AMSQueueItem();

            message.ResourceID = UuidHelper.NewUuidString();
            message.ItemType = AMSQueueItemType.StartEvent;

            return message;
        }
    }
}
