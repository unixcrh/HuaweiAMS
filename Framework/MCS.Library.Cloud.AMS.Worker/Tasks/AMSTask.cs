using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Tasks
{
    public static class AMSTask
    {
        public static async Task ExeucteAllTasks(CancellationToken cancellationToken)
        {
            await StartEventsInTimeFrame(cancellationToken);
            await StopEventsInTimeFrame(cancellationToken);
            await Task.Run(() =>
            {
                Task.Run(() =>
                {
                    Task.Delay(10000).Wait();
                    Trace.TraceInformation("delay");
                });
            });
        }

        /// <summary>
        /// 检查需要启动的事件
        /// </summary>
        /// <returns></returns>
        public static async Task StartEventsInTimeFrame(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                AMSEventCollection events = AMSEventSqlAdapter.Instance.LoadNeedStartEvents(TimeSpan.FromMinutes(5));

                AMSQueueItemCollection messages = new AMSQueueItemCollection();

                foreach (AMSEvent eventData in events)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    if (IsLockAvailable(eventData))
                        Trace.TraceInformation(eventData.ID);
                }

                GetQueue().AddMessages(string.Empty, messages.ToArray());
            });
        }

        /// <summary>
        /// 停止需要停止的任务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task StopEventsInTimeFrame(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                AMSEventCollection events = AMSEventSqlAdapter.Instance.LoadNeedStopEvents();

                AMSQueueItemCollection messages = new AMSQueueItemCollection();

                foreach (AMSEvent eventData in events)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;

                    if (IsLockAvailable(eventData))
                    {
                        messages.Add(CreateMessage(eventData, AMSQueueItemType.StopEvent));
                        Trace.TraceInformation(eventData.ID);
                    }
                }

                GetQueue().AddMessages(string.Empty, messages.ToArray());
            });
        }

        private static IQueue<AMSQueueItem> GetQueue()
        {
            return QueueManager.GetQueue<AMSQueueItem>("amsQueue");
        }

        private static AMSQueueItem CreateMessage(AMSEvent eventData, AMSQueueItemType itemType)
        {
            AMSQueueItem message = new AMSQueueItem();

            message.ItemType = itemType;
            message.ResourceID = eventData.ID;
            message.ResourceName = eventData.Name;

            return message;
        }

        private static AMSCheckLockResult ExtendLockTime(AMSEvent eventData)
        {
            AMSLock lockData = PrepareEventLock(eventData);

            return AMSLockSqlAdapter.Instance.ExtendLockTime(lockData);
        }

        private static bool IsLockAvailable(AMSEvent eventData)
        {
            AMSLock lockData = PrepareEventLock(eventData);

            AMSCheckLockResult lockResult = AMSLockSqlAdapter.Instance.AddLock(lockData);

            return lockResult.Available;
        }

        /// <summary>
        /// 准备事件相关的锁
        /// </summary>
        /// <returns></returns>
        private static AMSLock PrepareEventLock(AMSEvent eventData)
        {
            AMSLock lockData = new AMSLock();

            lockData.LockID = eventData.ID;
            lockData.LockType = AMSLockType.EventLock;
            lockData.Description = string.Format("为事件(ID: {0}, Name: {1})加锁", eventData.ID, eventData.Name);

            return lockData;
        }
    }
}
