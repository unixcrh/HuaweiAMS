using MCS.Library.Cloud.AMS.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Impl;

namespace MCS.Library.Cloud.AMS.Data.Adapters
{
    public class AMSChannelSqlAdapter : UpdatableAndLoadableAdapterBase<AMSChannel, AMSChannelCollection>, IAMSChannelAdapter
    {
        public AMSChannelCollection GetAllChannels()
        {
            throw new NotImplementedException();
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
