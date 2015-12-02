using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MediaServices.Client;

namespace MCS.Library.Cloud.AMSHelper.Configuration
{
    /// <summary>
    /// Media Service Account Name and key configuration element
    /// </summary>
    public class MediaServiceAccountConfigurationElement : NamedConfigurationElement
    {
        /// <summary>
        /// Media Service Account Key
        /// </summary>
        [ConfigurationProperty("accountKey", IsRequired = true)]
        public string AccountKey
        {
            get
            {
                return (string)this["accountKey"];
            }
        }
    }

    /// <summary>
    /// Media Service Account Name and key configuration element collection
    /// </summary>
    public class MediaServiceAccountConfigurationElementCollection : NamedConfigurationElementCollection<MediaServiceAccountConfigurationElement>
    {
        public MediaServicesCredentials GetCredentials(string configedName)
        {
            MediaServicesCredentials result = null;

            if (this.ContainsKey(configedName))
            {
                MediaServiceAccountConfigurationElement elem = this.CheckAndGet(configedName);

                result = new MediaServicesCredentials(elem.Name, elem.AccountKey);
            }

            return result;
        }

        public CloudMediaContext GetCloudMediaContext(string configedName)
        {
            MediaServicesCredentials credentials = this.GetCredentials(configedName);

            CloudMediaContext result = null;

            if (credentials != null)
                result = new CloudMediaContext(credentials);

            return result;
        }
    }
}
