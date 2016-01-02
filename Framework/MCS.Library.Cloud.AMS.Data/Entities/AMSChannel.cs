using MCS.Library.Core;
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
        [ORFieldMapping("CreateTime", UtcTimeToLocal = true)]
        public DateTime CreateTime
        {
            get;
            set;
        }

        [DataMember]
        public string PreviewUrl
        {
            get;
            set;
        }

        [DataMember]
        public string PrimaryInputUrl
        {
            get;
            set;
        }

        [DataMember]
        public string SecondaryInputUrl
        {
            get;
            set;
        }

        [DataMember]
        [SqlBehavior(EnumUsage = EnumUsageTypes.UseEnumString)]
        public AMSCDNPrefixMode CDNPrefixMode
        {
            get;
            set;
        }

        [DataMember]
        public string CDNPrefix
        {
            get;
            set;
        }

        [DataMember]
        public string AlternateCDNEndpoint
        {
            get;
            set;
        }

        public void FillStatusFromCloud(AMSChannel channelInCloud)
        {
            channelInCloud.NullCheck("channelInCloud");

            this.AMSID = channelInCloud.AMSID;
            this.Name = channelInCloud.Name;
            this.Description = channelInCloud.Description;
            this.State = channelInCloud.State;
            this.AMSLastModified = channelInCloud.AMSLastModified;
            this.PreviewUrl = channelInCloud.PreviewUrl;
            this.PrimaryInputUrl = channelInCloud.PrimaryInputUrl;
            this.SecondaryInputUrl = channelInCloud.SecondaryInputUrl;
        }
    }

    [Serializable]
    [DataContract]
    public class AMSChannelCollection : EditableDataObjectCollectionBase<AMSChannel>
    {
        public AMSChannel FindChannelByAMSID(string amsID)
        {
            return this.Find(c => string.Compare(c.AMSID, amsID, true) == 0);
        }

        public AMSChannel FindChannelByName(string name)
        {
            return this.Find(c => string.Compare(c.Name, name, true) == 0);
        }

        public AMSChannelCollection MergeFrom(AMSChannelCollection channelsWithStatus)
        {
            AMSChannelCollection result = new AMSChannelCollection();

            foreach (AMSChannel channel in this)
            {
                AMSChannel channelWithStatus = channelsWithStatus.FindChannelByAMSID(channel.AMSID);

                if (channelWithStatus != null)
                {
                    channel.FillStatusFromCloud(channelWithStatus);
                    result.Add(channel);
                }
                else
                    channel.State = AMSChannelState.Disabled;
            }

            foreach (AMSChannel channel in channelsWithStatus)
            {
                AMSChannel channelInResult = result.FindChannelByAMSID(channel.AMSID);

                if (channelInResult == null)
                    result.Add(channel);
            }

            return result;
        }
    }
}
