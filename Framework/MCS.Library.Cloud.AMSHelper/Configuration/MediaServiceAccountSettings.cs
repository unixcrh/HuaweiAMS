using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Configuration
{
    /// <summary>
    /// MediaService Account Settings
    /// </summary>
    public class MediaServiceAccountSettings : ConfigurationSection
    {
        public static MediaServiceAccountSettings GetConfig()
        {
            MediaServiceAccountSettings settings = (MediaServiceAccountSettings)ConfigurationBroker.GetSection("mediaServiceAccountSettings");

            if (settings == null)
                settings = new MediaServiceAccountSettings();

            return settings;
        }

        [ConfigurationProperty("accounts")]
        public MediaServiceAccountConfigurationElementCollection Accounts
        {
            get
            {
                return (MediaServiceAccountConfigurationElementCollection)this["accounts"];
            }
        }
    }
}
