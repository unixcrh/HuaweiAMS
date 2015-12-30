using MCS.Library.Core;
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

        /// <summary>
        /// Media Context Scope. It is 'urn:WindowsAzureMediaServices' in Mooncake
        /// </summary>
        [ConfigurationProperty("scope", IsRequired = false)]
        public string Scope
        {
            get
            {
                return (string)this["scope"];
            }
        }

        /// <summary>
        /// It is 'https://wamsprodglobal001acs.accesscontrol.chinacloudapi.cn' in Mooncake
        /// </summary>
        [ConfigurationProperty("acsBaseAddress", IsRequired = false)]
        public string AcsBaseAddress
        {
            get
            {
                return (string)this["acsBaseAddress"];
            }
        }

        /// <summary>
        /// It is 'https://wamsshaclus001rest-hs.chinacloudapp.cn/API/' or 'https://wamsbjbclus001rest-hs.chinacloudapp.cn/API/' in Mooncake
        /// </summary>
        [ConfigurationProperty("apiServerAddress", IsRequired = false)]
        public string ApiServerAddress
        {
            get
            {
                return (string)this["apiServerAddress"];
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

                result = new MediaServicesCredentials(elem.Name, elem.AccountKey, elem.Scope, elem.AcsBaseAddress);
            }

            return result;
        }

        public CloudMediaContext GetCloudMediaContext(string configedName)
        {
            MediaServicesCredentials credentials = this.GetCredentials(configedName);

            CloudMediaContext result = null;

            if (credentials != null)
            {
                string apiAddress = string.Empty;

                if (this.ContainsKey(configedName))
                    apiAddress = this[configedName].ApiServerAddress;

                if (apiAddress.IsNotEmpty())
                    result = new CloudMediaContext(new Uri(apiAddress), credentials);
                else
                    result = new CloudMediaContext(credentials);
            }

            return result;
        }
    }
}
