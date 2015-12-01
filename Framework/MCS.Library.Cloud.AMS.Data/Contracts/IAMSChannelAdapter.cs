using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Contracts
{
    /// <summary>
    /// 频道操作的Adapter
    /// </summary>
    public interface IAMSChannelAdapter
    {
        /// <summary>
        /// 获取所有的频道
        /// </summary>
        /// <returns></returns>
        AMSChannelCollection GetAllChannels();
    }
}
