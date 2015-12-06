using MCS.Library.Cloud.AMS.Data.Configuration;
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
    [ORTableMapping("AMS.Locks")]
    public class AMSLock
    {
        public AMSLock()
        {
            this.EffectiveTime = AMSLockSettings.GetConfig().DefaultEffectiveTime;
        }

        /// <summary>
		/// 锁ID
		/// </summary>
		[ORFieldMapping("LockID", PrimaryKey = true)]
        [DataMember]
        public string LockID { get; set; }

        /// <summary>
        /// 锁对应的资源ID
        /// </summary>
        [DataMember]
        public string ResourceID { get; set; }

        /// <summary>
        /// 上锁人ID
        /// </summary>
        [DataMember]
        public string LockPersonID
        {
            get;
            set;
        }

        /// <summary>
        /// 上锁人名称
        /// </summary>
        [DataMember]
        public string LockPersonName
        {
            get;
            set;
        }

        /// <summary>
        /// 上锁时间
        /// </summary>
        [DataMember]
        [SqlBehavior(DefaultExpression = "GETUTCDATE()")]
        public DateTime LockTime { get; set; }

        /// <summary>
        /// 锁有效时间
        /// </summary>
        [DataMember]
        public TimeSpan EffectiveTime { get; set; }

        /// <summary>
        /// 锁的类型
        /// </summary>
        [ORFieldMapping("LockType")]
        [SqlBehavior(EnumUsage = EnumUsageTypes.UseEnumString)]
        public AMSLockType LockType { get; set; }

        /// <summary>
        /// 锁的描述信息
        /// </summary>
        [DataMember]
        public string Description { get; set; }
    }
}
