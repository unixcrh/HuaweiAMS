using MCS.Library.Data.DataObjects;
using MCS.Library.Data.Mapping;
using MCS.Library.Validation;
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
        [StringLengthValidator(1, 128, MessageTemplate = "事件名称不能为空，且必须小于128个字符")]
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
        public string Speakers
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
        [SqlBehavior(BindingFlags = ClauseBindingFlags.Select | ClauseBindingFlags.Where)]
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

        [DataMember]
        public string AMSProgramID
        {
            get;
            set;
        }

        [DataMember]
        public string DefaultPlaybackUrl
        {
            get;
            set;
        }

        [DataMember]
        public string CDNPlaybackUrl
        {
            get;
            set;
        }

        [DataMember]
        public string PosterUrl
        {
            get;
            set;
        }

        [DataMember]
        public string LogoUrl
        {
            get;
            set;
        }

        [DataMember]
        public Decimal Rating
        {
            get;
            set;
        }

        [DataMember]
        public long Views
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
