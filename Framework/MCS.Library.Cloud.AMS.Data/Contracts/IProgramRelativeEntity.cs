using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Contracts
{
    /// <summary>
    /// AMS Program相关的实体
    /// </summary>
    public interface IProgramRelativeEntity
    {
        AMSEventState State
        {
            get;
            set;
        }

        string AMSProgramID
        {
            get;
            set;
        }

        string DefaultPlaybackUrl
        {
            get;
            set;
        }

        string CDNPlaybackUrl
        {
            get;
            set;
        }
    }
}
