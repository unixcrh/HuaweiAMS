using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Entities
{
    [Serializable]
    [DataContract]
    [ORTableMapping("AMS.UserViews")]
    public class AMSUserView
    {
        [ORFieldMapping("EventID", PrimaryKey = true)]
        public string EventID
        {
            get;
            set;
        }

        [ORFieldMapping("UserID", PrimaryKey = true)]
        public string UserID
        {
            get;
            set;
        }

        [ORFieldMapping("UserName")]
        public string UserName
        {
            get;
            set;
        }

        [DataMember]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        public DateTime CreateTime
        {
            get;
            set;
        }

        public string LastClientAccessIP
        {
            get;
            set;
        }

        [DataMember]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where, DefaultExpression = "GETUTCDATE()")]
        [ORFieldMapping("StartTime", UtcTimeToLocal = true)]
        public DateTime LastAccessTime
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AMSUserViewCollection : EditableDataObjectCollectionBase<AMSUserView>
    {
    }
}
