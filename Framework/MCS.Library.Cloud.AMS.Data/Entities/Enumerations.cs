using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Entities
{
    public enum AMSChannelState
    {
        /// <summary>
        /// 
        /// </summary>
        Stopped = 0,

        /// <summary>
        /// 
        /// </summary>
        Starting = 1,

        /// <summary>
        /// 
        /// </summary>
        Running = 2,

        /// <summary>
        /// 
        /// </summary>
        Stopping = 3,

        /// <summary>
        /// 
        /// </summary>
        Deleting = 4,

        /// <summary>
        /// 
        /// </summary>
        Disabled = 5
    }

    public enum AMSEventState
    {
        /// <summary>
        /// 
        /// </summary>
        Stopped = 0,

        /// <summary>
        /// 
        /// </summary>
        Starting = 1,

        /// <summary>
        /// 
        /// </summary>
        Running = 2,

        /// <summary>
        /// 
        /// </summary>
        Stopping = 3,

        /// <summary>
        /// 
        /// </summary>
        Disabled = 4
    }

    /// <summary>
    /// 锁的类型
    /// </summary>
    public enum AMSLockType
    {
        None = 0,

        [EnumItemDescription("事件操作锁")]
        EventLock = 1,
    }

    public enum AMSCheckLockStatus
    {
        [EnumItemDescription("没有上锁")]
        NotLocked,

        [EnumItemDescription("上锁但是已经过期")]
        LockExpired,

        [EnumItemDescription("已经上锁")]
        Locked
    }
}
