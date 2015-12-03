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
    [ORTableMapping("AMS.Events")]
    public class AMSEvent
    {
        [DataMember]
        [ORFieldMapping("ID", PrimaryKey = true)]
        public string ID
        {
            get;
            set;
        }

        /// <summary>
        /// 所对应的频道的ID
        /// </summary>
        [DataMember]
        public string ChannelID
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public string Description
        {
            get;
            set;
        }

        [DataMember]
        public DateTime StartTime
        {
            get;
            set;
        }

        [DataMember]
        public DateTime EndTime
        {
            get;
            set;
        }

        [DataMember]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [DataMember]
        public string CreatorID
        {
            get;
            set;
        }

        [DataMember]
        [SqlBehavior(EnumUsage = EnumUsageTypes.UseEnumString)]
        public AMSEventState State
        {
            get;
            set;
        }

        [DataMember]
        public string CreatorName
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AMSEventCollection : EditableDataObjectCollectionBase<AMSEvent>
    {
    }
}
