using MCS.Library.Cloud.AMSHelper.Configuration;
using MCS.Library.Cloud.AMSHelper.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Mechanism
{
    public static class LiveChannelManager
    {
        private static Channelnfo GetChannelInfo(string configedName)
        {
            LiveChannelConfigurationElement channelElem = LiveChannelSettings.GetConfig().Channels.CheckAndGet(configedName);

            MediaServiceAccountConfigurationElement accountElem = MediaServiceAccountSettings.GetConfig().Accounts.CheckAndGet(channelElem.AccountName);

            return null;
        }

        //private static MediaServicesCredentials
    }
}
