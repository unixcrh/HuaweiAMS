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
    [DataContract]
    [Serializable]
    [ORTableMapping("AMS.Queue")]
    public class AMSQueueItem
    {
        [DataMember]
        [ORFieldMapping("ID", IsIdentity = true, PrimaryKey = true)]
        public long ID
        {
            get;
            set;
        }

        [DataMember]
        public string Category
        {
            get;
            set;
        }

        [DataMember]
        public string ResourceID
        {
            get;
            set;
        }

        [DataMember]
        public AMSQueueItemType ItemType
        {
            get;
            set;
        }

        [DataMember]
        public string ResourceName
        {
            get;
            set;
        }

        [DataMember]
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
        public DateTime CreateTime
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    public class AMSQueueItemCollection : EditableDataObjectCollectionBase<AMSQueueItem>
    {
    }
}
