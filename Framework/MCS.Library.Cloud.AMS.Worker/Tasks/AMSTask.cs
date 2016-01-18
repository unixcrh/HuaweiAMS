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
                            }
                            catch (System.Exception ex)
                            {
                                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Error, 60010, ex.ToString());
                            }
                            finally
                            {
                                Task.Delay(timeInterval).Wait();
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
                    messages.Add(eventData.ToQueueMessage(AMSQueueItemType.StartEvent));

                    TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60011, "Add start new event {0} to queue.", eventData.ID);
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
                    messages.Add(eventData.ToQueueMessage(AMSQueueItemType.StopEvent));
                    TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60012, "Add stop new event {0} to queue.", eventData.ID);
                }
            }

            GetQueue().AddMessages(string.Empty, messages.ToArray());
        }

        public static void ReadQueue(CancellationToken cancellationToken)
        {
            AMSQueueItem message = GetQueue().GetMessages(string.Empty).SingleOrDefault();

            if (message != null)
            {
                //Trace.TraceInformation("Message: ID={0}, ResourceID={1}, Name={2}, ItemType={3}",
                //    message.ID, message.ResourceID, message.ResourceID, message.ItemType);

                if (AMSWorkerSettings.GetConfig().ItemTypes.IsEnabled(message.ItemType))
                {
                    switch (message.ItemType)
                    {
                        case AMSQueueItemType.StartEvent:
                            RunQueueTask(message, cancellationToken, AMSOperations.StartEvent);
                            break;
                        case AMSQueueItemType.StopEvent:
                            RunQueueTask(message, cancellationToken, AMSOperations.StopEvent);
                            break;
                        case AMSQueueItemType.SyncChannelInfo:
                            RunQueueTask(message, cancellationToken, AMSOperations.SyncChannelInfo);
                            break;
                        case AMSQueueItemType.StopChannel:
                            RunQueueTask(message, cancellationToken, AMSOperations.StopChannel);
                            break;
                        case AMSQueueItemType.DeleteProgram:
                            RunQueueTask(message, cancellationToken, AMSOperations.DeleteProgram);
                            break;
                    }
                }
            }
        }

        private static IQueue<AMSQueueItem> GetQueue()
        {
            return QueueManager.GetQueue<AMSQueueItem>("amsQueue");
        }

        private static void RunQueueTask(AMSQueueItem message, CancellationToken cancellationToken, Action<AMSQueueItem, CancellationToken> action)
        {
            Task.Run(() =>
            {
                try
                {
                    action(message, cancellationToken);
                }
                catch (System.Exception ex)
                {
                    TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Error, 60014, ex.ToString());
                }
            });
        }
    }
}
