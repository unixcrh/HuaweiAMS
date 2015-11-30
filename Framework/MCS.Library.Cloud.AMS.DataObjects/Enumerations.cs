using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.DataObjects
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
        Deleting = 4
    }
}
