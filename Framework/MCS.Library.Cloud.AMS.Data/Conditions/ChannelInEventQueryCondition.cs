using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Conditions
{
    [Serializable]
    public class ChannelInEventQueryCondition
    {
        [ConditionMapping(DataFieldName = "EventID")]
        public string EventID
        {
            get;
            set;
        }
    }
}
