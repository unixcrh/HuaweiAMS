using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Data.Executors;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Impl.Executors
{
    public abstract class AMSChannelInEventExecutorBase : AMSExecutorBase
    {
        public AMSChannelInEventExecutorBase(string eventID, IEnumerable<string> channelIDs, AMSOperationType operationType) : base(operationType)
        {
            eventID.CheckStringIsNullOrEmpty("eventID");
            channelIDs.NullCheck("channelIDs");

            this.EventID = eventID;
            this.ChannelIDs = channelIDs;
        }

        public string EventID
        {
            get;
            private set;
        }

        public IEnumerable<string> ChannelIDs
        {
            get;
            private set;
        }

        protected override void PrepareOperationLog(AMSOperationContext context)
        {
            UserOperationLog log = new UserOperationLog();

            log.Subject = this.GetOperationDescription();
            log.ResourceID = this.EventID.ToString();

            log.FillHttpContext();

            context.Logs.Add(log);
        }

        protected string GetChannelIDsDescription()
        {
            StringBuilder strB = new StringBuilder();

            foreach(string channelID in this.ChannelIDs)
            {
                if (strB.Length > 0)
                    strB.Append(",");

                strB.Append(channelID);
            }

            return strB.ToString();
        }
    }
}
