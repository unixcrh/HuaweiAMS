using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Executors
{
    public enum AMSOperationType
    {
        /// <summary>
        /// 无操作
        /// </summary>
        None = 0,

        /// <summary>
        /// 编辑事件
        /// </summary>
        [EnumItemDescription("编辑事件")]
        EditEvent,

        /// <summary>
        /// 编辑事件
        /// </summary>
        [EnumItemDescription("删除事件")]
        DeleteEvent,

        [EnumItemDescription("在事件下增加频道")]
        AddChannelInEvent,

        [EnumItemDescription("在事件下删除频道")]
        DeleteChannelsInEvent
    }
}
