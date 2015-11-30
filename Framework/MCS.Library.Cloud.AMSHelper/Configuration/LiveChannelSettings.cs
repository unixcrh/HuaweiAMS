using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Configuration
{
    public class LiveChannelSettings : ConfigurationSection
    {
        public static LiveChannelSettings GetConfig()
        {
            LiveChannelSettings settings = (LiveChannelSettings)ConfigurationBroker.GetSection("liveChannelSettings");

            if (settings == null)
                settings = new LiveChannelSettings();

            return settings;
        }

        [ConfigurationProperty("channels")]
        public LiveChannelConfigurationElementCollection Channels
        {
            get
            {
                return (LiveChannelConfigurationElementCollection)this["channels"];
            }
        }
    }
}
