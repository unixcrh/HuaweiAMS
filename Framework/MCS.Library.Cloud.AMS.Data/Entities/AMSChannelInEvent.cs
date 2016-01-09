using MCS.Library.Data.DataObjects;
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
    public class AMSChannelInEvent : AMSChannel
    {
        public bool IsDefault
        {
            get;
            set;
        }
    }

    [Serializable]
    [DataContract]
    public class AMSChannelInEventCollection : EditableDataObjectCollectionBase<AMSChannelInEvent>
    {
    }
}
