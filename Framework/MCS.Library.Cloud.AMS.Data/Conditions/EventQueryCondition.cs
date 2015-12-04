using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Conditions
{
    [Serializable]
    public class EventQueryCondition
    {
        [ConditionMapping(DataFieldName = "Name", EscapeLikeString = true, Prefix = "%", Postfix = "%", Operation = "LIKE")]
        public string Name
        {
            get;
            set;
        }

        [ConditionMapping(DataFieldName = "ChannelID")]
        public string ChannelID
        {
            get;
            set;
        }
    }
}
