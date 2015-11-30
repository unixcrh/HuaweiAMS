using MCS.Library.Cloud.AMS.DataObjects;
using MCS.Library.Cloud.AMSHelper.Configuration;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Mechanism
{
    public static class LiveChannelManager
    {       
        private static AMSChannel GetChannelInfo(string configedName)
        {
            LiveChannelConfigurationElement channelElem = LiveChannelSettings.GetConfig().Channels.CheckAndGet(configedName);

            MediaServiceAccountConfigurationElement accountElem = MediaServiceAccountSettings.GetConfig().Accounts.CheckAndGet(channelElem.AccountName);

            return null;
        }
    }
}
