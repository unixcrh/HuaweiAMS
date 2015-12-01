using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using System;
using System.Runtime.Serialization;

namespace MCS.Library.Cloud.AMS.Data.Entities
{
    [Serializable]
    [DataContract]
    [ORTableMapping("AMS.Channels")]
    public class AMSChannel
    {
        [DataMember]
        [ORFieldMapping("ID", PrimaryKey = true)]
        public string ID
        {
            get;
            set;
        }

        [DataMember]
        public string AMSID
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
        public string AMSAccountName
        {
            get;
            set;
        }

        [DataMember]
        [SqlBehavior(EnumUsage = EnumUsageTypes.UseEnumString)]
        public AMSChannelState State
        {
            get;
            set;
        }

        [DataMember]
        public DateTime AMSLastModified
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

    [Serializable]
    [DataContract]
    public class AMSChannelCollection : EditableDataObjectCollectionBase<AMSChannel>
    {
    }
}
