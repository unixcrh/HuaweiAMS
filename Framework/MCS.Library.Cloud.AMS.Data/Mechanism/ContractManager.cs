using MCS.Library.Cloud.AMS.Data.Configuration;
using MCS.Library.Cloud.AMS.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Mechanism
{
    public static class ContractManager
    {
        public static IAMSChannelAdapter GetAMSChannelAdapter()
        {
            return AMSDataAdapterSettings.GetConfig().TypeFactories.CheckAndGetInstance<IAMSChannelAdapter>("amsChannelAdapter");
        }
    }
}
