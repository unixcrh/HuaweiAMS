using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Data.Executors;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Executors
{
    public class AMSDeleteChannelsInEventExecutor : AMSChannelInEventExecutorBase
    {
        public AMSDeleteChannelsInEventExecutor(string eventID, params string[] channelIDs)
            : base(eventID, channelIDs, AMSOperationType.DeleteChannelsInEvent)
        {
        }

        protected override object DoOperation(AMSOperationContext context)
        {
            return AMSEventSqlAdapter.Instance.DeleteChannels(this.EventID, this.ChannelIDs);
        }

        protected override string GetOperationDescription()
        {
            string opDesp = EnumItemDescriptionAttribute.GetDescription(OperationType);

            string entityDesp = string.Format("在事件{0}中删除频道{1}", this.EventID, this.GetChannelIDsDescription());

            return string.Format("{0}-{1}", opDesp, entityDesp);
        }
    }
}
