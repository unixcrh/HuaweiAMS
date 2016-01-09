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
    public class AMSAddChannelInEventExecutor : AMSChannelInEventExecutorBase
    {

        public AMSAddChannelInEventExecutor(string eventID, string channelID)
            : base(eventID, new string[] { channelID }, AMSOperationType.AddChannelInEvent)
        {
        }

        protected override object DoOperation(AMSOperationContext context)
        {
            try
            {
                return AMSEventSqlAdapter.Instance.AddChannel(this.EventID, this.ChannelIDs);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.ErrorCode == 2627)
                    throw new SystemSupportException(string.Format("在事件{0}中不能增加重复的频道", this.EventID));
                else
                    throw;
            }
        }

        protected override string GetOperationDescription()
        {
            string opDesp = EnumItemDescriptionAttribute.GetDescription(OperationType);

            string entityDesp = string.Format("在事件{0}中增加频道{1}", this.EventID, this.GetChannelIDsDescription());

            return string.Format("{0}-{1}", opDesp, entityDesp);
        }
    }
}
