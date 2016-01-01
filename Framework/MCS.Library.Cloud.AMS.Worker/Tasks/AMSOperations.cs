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

                    AMSChannel channel = StartChannel(eventData.ChannelID, cancellationToken);

                    if (cancellationToken.IsCancellationRequested == false)
                    {
                        if (channel != null && channel.State == AMSChannelState.Running)
                        {
                            LockHelper.ExtendLockTime(eventData);

                            StartProgram(eventData, channel, cancellationToken);
                        }
                    }
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

                    StopProgram(eventData, cancellationToken);
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

                LiveChannelManager.StopChannel(channel);
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

                LiveChannelManager.StartChannel(channel);
                AMSChannelSqlAdapter.Instance.Update(channel);

                TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60016, "Channel Started:\n{0}", channel.ToTraceInfo());
            }

            return channel;
        }

        private static void StartProgram(AMSEvent eventData, AMSChannel channel, CancellationToken cancellationToken)
        {
            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60017, "Start Program:\n{0}", channel.ToTraceInfo());

            LiveChannelManager.StartProgram(channel, eventData);

            AMSEventSqlAdapter.Instance.Update(eventData);

            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60017, "Program Started:\n{0}", channel.ToTraceInfo());
        }

        private static void StopProgram(AMSEvent eventData, CancellationToken cancellationToken)
        {
            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60018, "Stop Program:\n{0}", eventData.ToTraceInfo());

            AMSChannel channel = AMSChannelSqlAdapter.Instance.LoadByID(eventData.ChannelID);

            if (channel != null)
                LiveChannelManager.StopProgram(channel, eventData);
            else
                eventData.State = AMSEventState.Completed;

            AMSEventSqlAdapter.Instance.Update(eventData);

            TraceHelper.AMSTaskTraceSource.TraceEvent(TraceEventType.Verbose, 60018, "Program Stopped:\n{0}", eventData.ToTraceInfo());
        }
    }
}
