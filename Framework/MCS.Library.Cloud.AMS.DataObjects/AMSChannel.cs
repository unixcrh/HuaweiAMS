using MCS.Library.Data.DataObjects;
using System;
using System.Runtime.Serialization;

namespace MCS.Library.Cloud.AMS.DataObjects
{
    [Serializable]
    [DataContract]
    public class AMSChannel
    {
        [DataMember]
        public string ID
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
        public AMSChannelState State
        {
            get;
            set;
        }

        [DataMember]
        public DateTime LastModified
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
