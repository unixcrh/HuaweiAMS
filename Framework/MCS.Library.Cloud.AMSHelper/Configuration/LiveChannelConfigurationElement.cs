using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Configuration
{
    public class LiveChannelConfigurationElement : NamedConfigurationElement
    {
        /// <summary>
        /// Live Channel Name
        /// </summary>
        [ConfigurationProperty("channelName", IsRequired = true)]
        public string ChannelName
        {
            get
            {
                return (string)this["channelName"];
            }
        }

        /// <summary>
        /// Media Service Account Name
        /// </summary>
        [ConfigurationProperty("accountName", IsRequired = true)]
        public string AccountName
        {
            get
            {
                return (string)this["accountName"];
            }
        }
    }

    public class LiveChannelConfigurationElementCollection : NamedConfigurationElementCollection<LiveChannelConfigurationElement>
    {
    }
}
