using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Worker.Configuration;
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
        private static readonly TimeSpan DefaultDelayTime = TimeSpan.FromSeconds(1);

        public static Task DoLoopTask(Action<CancellationToken> action, TimeSpan timeInterval, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
                    {
                        while (cancellationToken.IsCancellationRequested == false)
                        {
                            try
                            {
                                action(cancellationToken);

                                Task.Delay(timeInterval).Wait();
                            }
                            catch (System.Exception ex)
                            {
                                Trace.TraceError(ex.ToString());
                            }
                        }
                    });
        }

        public static void StartAllTasks(CancellationToken cancellationToken)
        {
            AMSWorkerSettings settings = AMSWorkerSettings.GetConfig();

            DoLoopTask(StartEventsInTimeFrame, DefaultDelayTime, cancellationToken);
            DoLoopTask(StopEventsInTimeFrame, DefaultDelayTime, cancellationToken);
            DoLoopTask(ReadQueue, DefaultDelayTime, cancellationToken);

            DoLoopTask(GenerateSyncChannelInfoMessages, settings.Durations.GetDuration("syncChannelInfoInterval", DefaultDelayTime), cancellationToken);
            DoLoopTask(GenerateStopChannelMessages, settings.Durations.GetDuration("stopChannelInterval", DefaultDelayTime), cancellationToken);
            DoLoopTask(GenerateDeleteProgramMessages, settings.Durations.GetDuration("deleteProgamInterval", DefaultDelayTime), cancellationToken);
        }

        public static void GenerateSyncChannelInfoMessages(CancellationToken cancellationToken)
        {
            AMSQueueItem message = new AMSQueueItem();

            message.ItemType = AMSQueueItemType.SyncChannelInfo;

            GetQueue().AddMessages(string.Empty, message);
        }

        public static void GenerateStopChannelMessages(CancellationToken cancellationToken)
        {
            AMSWorkerSettings settings = AMSWorkerSettings.GetConfig();

            AMSChannelCollection channels =
                AMSChannelSqlAdapter.Instance.LoadNeedStopChannels(settings.Durations.GetDuration("stopChannelLeadTime", TimeSpan.FromHours(1)));

            AMSQueueItemCollection messages = new AMSQueueItemCollection();

            foreach (AMSChannel channel in channels)
            {
                AMSQueueItem message = new AMSQueueItem();

                message.ItemType = AMSQueueItemType.StopChannel;
                message.ResourceID = channel.ID;
                message.ResourceName = channel.Name;

                messages.Add(message);
            }

            GetQueue().AddMessages(string.Empty, messages.ToArray());
        }

        public static void GenerateDeleteProgramMessages(CancellationToken cancellationToken)
        {
            AMSQueueItem message = new AMSQueueItem();

            message.ItemType = AMSQueueItemType.DeleteProgram;

            GetQueue().AddMessages(string.Empty, message);
        }

        /// <summary>
        /// 检查需要启动的事件
        /// </summary>
        /// <returns></returns>
        public static void StartEventsInTimeFrame(CancellationToken cancellationToken)
        {
            AMSWorkerSettings settings = AMSWorkerSettings.GetConfig();
            AMSEventCollection events = AMSEventSqlAdapter.Instance.LoadNeedStartEvents(settings.Durations.GetDuration("createChannelWarmup", TimeSpan.FromMinutes(20)));

            AMSQueueItemCollection messages = new AMSQueueItemCollection();

            foreach (AMSEvent eventData in events)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                if (LockHelper.IsLockAvailable(eventData))
                {
                    messages.Add(CreateMessage(eventData, AMSQueueItemType.StartEvent));

                    Trace.TraceInformation("Add start new event {0} to queue.", eventData.ID);
                }
            }

            GetQueue().AddMessages(string.Empty, messages.ToArray());
        }

        /// <summary>
        /// 停止需要停止的任务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static void StopEventsInTimeFrame(CancellationToken cancellationToken)
        {
            AMSEventCollection events = AMSEventSqlAdapter.Instance.LoadNeedStopEvents();

            AMSQueueItemCollection messages = new AMSQueueItemCollection();

            foreach (AMSEvent eventData in events)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                if (LockHelper.IsLockAvailable(eventData))
                {
                    messages.Add(CreateMessage(eventData, AMSQueueItemType.StopEvent));
                    Trace.TraceInformation("Add stop new event {0} to queue.", eventData.ID);
                }
            }

            GetQueue().AddMessages(string.Empty, messages.ToArray());
        }

        public static void ReadQueue(CancellationToken cancellationToken)
        {
            AMSQueueItem message = GetQueue().GetMessages(string.Empty).SingleOrDefault();

            if (message != null)
            {
                Trace.TraceInformation("Message: ID={0}, ResourceID={1}, Name={2}, ItemType={3}",
                    message.ID, message.ResourceID, message.ResourceID, message.ItemType);

                if (AMSWorkerSettings.GetConfig().ItemTypes.IsEnabled(message.ItemType))
                {
                    switch (message.ItemType)
                    {
                        case AMSQueueItemType.StartEvent:
                            AMSOperations.StartEvent(message.ResourceID, cancellationToken);
                            break;
                        case AMSQueueItemType.StopEvent:
                            AMSOperations.StopEvent(message.ResourceID, cancellationToken);
                            break;
                        case AMSQueueItemType.SyncChannelInfo:
                            AMSOperations.SyncChannelInfo(cancellationToken);
                            break;
                        case AMSQueueItemType.StopChannel:
                            AMSOperations.StopChannel(message.ResourceID, cancellationToken);
                            break;
                        case AMSQueueItemType.DeleteProgram:
                            AMSOperations.DeleteProgram(cancellationToken);
                            break;
                    }
                }
            }
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
    }
}
