using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
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
        public static void StartEvent(string eventID, CancellationToken cancellationToken)
        {
            AMSEvent eventData = null;
            try
            {
                eventData = AMSEventSqlAdapter.Instance.LoadByID(eventID);

                if (eventData != null)
                {
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
            catch (System.Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
            finally
            {
                if (eventData != null)
                    LockHelper.Unlock(eventData);
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
                Trace.TraceInformation("启动频道:\n{0}", channel.ToTraceInfo());

                LiveChannelManager.StartChannel(channel);
                AMSChannelSqlAdapter.Instance.Update(channel);

                Trace.TraceInformation("频道已启动:\n{0}", channel.ToTraceInfo());
            }

            return channel;
        }

        private static void StartProgram(AMSEvent eventData, AMSChannel channel, CancellationToken cancellationToken)
        {
            Trace.TraceInformation("启动节目:\n{0}", channel.ToTraceInfo());

            LiveChannelManager.StartProgram(channel, eventData);

            Trace.TraceInformation("节目已启动:\n{0}", channel.ToTraceInfo());
        }
    }
}
