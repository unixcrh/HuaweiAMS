using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Worker.Configuration;
using MCS.Library.Cloud.AMSHelper.Mechanism;
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
    public static class AMSOperations
    {
        public static void StartEvent(AMSQueueItem message, CancellationToken cancellationToken)
        {
            AMSEvent eventData = null;
            try
            {
                eventData = AMSEventSqlAdapter.Instance.LoadByID(message.ResourceID);

                if (eventData != null)
                {
                    if (eventData.State == AMSEventState.NotStart)
                        AMSEventSqlAdapter.Instance.UpdateState(eventData.ID, AMSEventState.Starting);

                    AMSEventChannelCollection ecs = AMSEventSqlAdapter.Instance.LoadEventAndChannel(eventData.ID);

                    Task[] startChannelTasks = new Task[ecs.Count];

                    for (int i = 0; i < startChannelTasks.Length; i++)
                    {
                        AMSEventChannel ec = ecs[i];
                        startChannelTasks[i] = Task.Factory.StartNew(() => StartOneChannelAndProgram(eventData, ec, cancellationToken));
                    }

                    Task.WaitAll(startChannelTasks);

                    AMSEventSqlAdapter.Instance.UpdateRunningStateByChannels(eventData.ID);
                }
            }
            finally
            {
                if (eventData != null)
                    LockHelper.Unlock(eventData);
            }
        }

        public static void StopEvent(AMSQueueItem message, CancellationToken cancellationToken)
        {
            AMSEvent eventData = null;
            try
            {
                eventData = AMSEventSqlAdapter.Instance.LoadByID(message.ResourceID);

                if (eventData != null)
                {
                    if (eventData.State == AMSEventState.Running)
                        AMSEventSqlAdapter.Instance.UpdateState(eventData.ID, AMSEventState.Stopping);

                    AMSEventChannelCollection ecs = AMSEventSqlAdapter.Instance.LoadEventAndChannel(eventData.ID);

                    Task[] stopProgramTasks = new Task[ecs.Count];

                    for (int i = 0; i < stopProgramTasks.Length; i++)
                    {
                        AMSEventChannel ec = ecs[i];
                        stopProgramTasks[i] = Task.Factory.StartNew(() => StopOneProgram(eventData, ec, cancellationToken));
                    }

                    Task.WaitAll(stopProgramTasks.ToArray());

                    AMSEventSqlAdapter.Instance.UpdateCompletedStateByChannels(eventData.ID);
                }
            }
            finally
            {
                if (eventData != null)
                    LockHelper.Unlock(eventData);
            }
        }

        public static void SyncChannelInfo(AMSQueueItem message, CancellationToken cancellationToken)
        {
            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60013, "Start Sync Channel Info");

            AMSChannelCollection channels = LiveChannelManager.GetAllChannels(true);

            AMSChannelSqlAdapter.Instance.UpdateAllChannels(channels);

            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60013, "Complete Sync Channel Info");
        }

        public static void StopChannel(AMSQueueItem message, CancellationToken cancellationToken)
        {
            AMSChannel channel = AMSChannelSqlAdapter.Instance.LoadByID(message.ResourceID);

            if (channel != null)
            {
                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60014, "Stop Channel:\n{0}", channel.ToTraceInfo());

                if (channel.State == AMSChannelState.Stopped)
                    AMSChannelSqlAdapter.Instance.UpdateState(channel.ID, AMSChannelState.Stopping);

                SimulateOrExecuteAction(() => LiveChannelManager.StopChannel(channel), () => channel.State = AMSChannelState.Stopped);

                AMSChannelSqlAdapter.Instance.Update(channel);

                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60014, "Channel Stopped:\n{0}", channel.ToTraceInfo());
            }
        }

        public static void DeleteProgram(AMSQueueItem message, CancellationToken cancellationToken)
        {
            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60015, "Delete expired programs");

            int count = LiveChannelManager.DeleteAllExpiredPrograms(AMSWorkerSettings.GetConfig().Durations.GetDuration("programExpireTime", TimeSpan.FromDays(1)));

            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60015, "{0} programs deleted", count);
        }

        /// <summary>
        /// 启动事件中的一个频道和节目
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="ec"></param>
        /// <param name="cancellationToken"></param>
        private static void StartOneChannelAndProgram(AMSEvent eventData, AMSEventChannel ec, CancellationToken cancellationToken)
        {
            try
            {
                if (ec.State == AMSEventState.NotStart)
                    AMSEventSqlAdapter.Instance.UpdateEventChannelState(ec.EventID, ec.ChannelID, AMSEventState.Starting);

                AMSChannel channel = StartChannel(ec.ChannelID, cancellationToken);

                if (cancellationToken.IsCancellationRequested == false)
                {
                    if (channel != null && channel.State == AMSChannelState.Running)
                    {
                        LockHelper.ExtendLockTime(eventData);

                        StartProgram(eventData, channel, ec, cancellationToken);
                    }
                }
            }
            catch (System.Exception ex)
            {
                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Error, 60020, ex.ToString());
            }
        }

        /// <summary>
        /// 停止一个节目
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="ec"></param>
        /// <param name="cancellationToken"></param>
        private static void StopOneProgram(AMSEvent eventData, AMSEventChannel ec, CancellationToken cancellationToken)
        {
            try
            {
                if (ec.State == AMSEventState.Running)
                    AMSEventSqlAdapter.Instance.UpdateEventChannelState(ec.EventID, ec.ChannelID, AMSEventState.Stopping);

                StopProgram(eventData, ec, cancellationToken);
            }
            catch (System.Exception ex)
            {
                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Error, 60021, ex.ToString());
            }
        }

        /// <summary>
        /// 启动频道，并且更新频道状态
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static AMSChannel StartChannel(string channelID, CancellationToken cancellationToken)
        {
            AMSChannel channel = AMSChannelSqlAdapter.Instance.LoadByID(channelID);

            if (channel != null)
            {
                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60016, "Start Channel:\n{0}", channel.ToTraceInfo());

                if (channel.State == AMSChannelState.Stopped)
                    AMSChannelSqlAdapter.Instance.UpdateState(channel.ID, AMSChannelState.Starting);

                SimulateOrExecuteAction(() => LiveChannelManager.StartChannel(channel), () => channel.State = AMSChannelState.Running);
                AMSChannelSqlAdapter.Instance.Update(channel);

                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60016, "Channel Started:\n{0}", channel.ToTraceInfo());
            }

            return channel;
        }

        private static void StartProgram(AMSEvent eventData, AMSChannel channel, AMSEventChannel ec, CancellationToken cancellationToken)
        {
            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60017, "Start Program:\n{0}", channel.ToTraceInfo());

            SimulateOrExecuteAction(() => LiveChannelManager.StartProgram(channel, eventData, ec), () => ec.State = AMSEventState.Running);

            AMSEventSqlAdapter.Instance.UpdateEventChannel(ec);

            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60017, "Program Started:\n{0}", channel.ToTraceInfo());
        }

        private static void StopProgram(AMSEvent eventData, AMSEventChannel ec, CancellationToken cancellationToken)
        {
            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60018, "Stop Program:\n{0}", eventData.ToTraceInfo());

            AMSChannel channel = AMSChannelSqlAdapter.Instance.LoadByID(ec.ChannelID);

            if (channel != null)
                SimulateOrExecuteAction(() => LiveChannelManager.StopProgram(channel, eventData, ec), () => ec.State = AMSEventState.Completed);
            else
                ec.State = AMSEventState.Completed;

            AMSEventSqlAdapter.Instance.UpdateEventChannel(ec);

            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60018, "Program Stopped:\n{0}", eventData.ToTraceInfo());
        }

        private static void SimulateOrExecuteAction(Action action, Action afterDelayAction = null)
        {
            SimulateOrExecuteAction(action, TimeSpan.FromSeconds(5), afterDelayAction);
        }

        private static void SimulateOrExecuteAction(Action action, TimeSpan delay, Action afterDelayAction = null)
        {
            if (AMSWorkerSettings.GetConfig().EnableSimulation == false)
            {
                action();
            }
            else
            {
                Task.Delay(delay).Wait();

                if (afterDelayAction != null)
                    afterDelayAction();
            }
        }
    }
}
