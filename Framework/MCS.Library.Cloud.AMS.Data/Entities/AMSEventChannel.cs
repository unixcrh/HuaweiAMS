using MCS.Library.Cloud.AMS.Data.Contracts;
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
    [ORTableMapping("AMS.EventsChannels")]
    public class AMSEventChannel : IProgramRelativeEntity
    {
        [DataMember]
        [ORFieldMapping("EventID", PrimaryKey = true)]
        public string EventID
        {
            get;
            set;
        }

        [DataMember]
        [ORFieldMapping("ChannelID", PrimaryKey = true)]
        public string ChannelID
        {
            get;
            set;
        }

        public string AMSProgramID
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

        public string DefaultPlaybackUrl
        {
            get;
            set;
        }

        public string CDNPlaybackUrl
        {
            get;
            set;
        }

        public bool IsDefault
        {
            get;
            set;
        }

        public static AMSEventChannel FromAMSEvent(AMSEvent eventData)
        {
            AMSEventChannel eventChannel = new AMSEventChannel();

            eventChannel.EventID = eventData.ID;
            eventChannel.ChannelID = eventData.ChannelID;
            eventChannel.IsDefault = true;
            eventChannel.AMSProgramID = eventData.AMSProgramID;
            eventChannel.State = eventData.State;
            eventChannel.DefaultPlaybackUrl = eventData.DefaultPlaybackUrl;
            eventChannel.CDNPlaybackUrl = eventData.CDNPlaybackUrl;

            return eventChannel;
        }
    }

    [Serializable]
    [DataContract]
    public class AMSEventChannelCollection : EditableDataObjectCollectionBase<AMSEventChannel>
    {
    }
}
