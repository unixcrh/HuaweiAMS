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
        NotStart = 0,

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
        Completed = 4
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

    public enum AMSQueueItemType
    {
        /// <summary>
        /// 启东事件
        /// </summary>
        StartEvent,

        /// <summary>
        /// 停止事件
        /// </summary>
        StopEvent,

        /// <summary>
        /// 同步频道信息
        /// </summary>
        SyncChannelInfo,

        /// <summary>
        /// 停用频道
        /// </summary>
        StopChannel,

        /// <summary>
        /// 删除节目
        /// </summary>
        DeleteProgram
    }

    /// <summary>
    /// CDN前缀的模式
    /// </summary>
    public enum AMSCDNPrefixMode
    {
        /// <summary>
        /// 无操作
        /// </summary>
        None,

        /// <summary>
        /// 在域名中添加前缀
        /// </summary>
        Prefix,

        /// <summary>
        /// 替换整个域名
        /// </summary>
        Host
    }
}
