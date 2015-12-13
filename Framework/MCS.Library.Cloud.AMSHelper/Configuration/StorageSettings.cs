using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Configuration
{
    public class StorageSettings : DeluxeConfigurationSection
    {
        public static StorageSettings GetConfig()
        {
            StorageSettings settings = (StorageSettings)ConfigurationBroker.GetSection("storageSettings");

            if (settings == null)
                settings = new StorageSettings();

            return settings;
        }

        [ConfigurationProperty("connectionStrings")]
        public StorageConnectionStringConfigurationElementCollection ConnectionStrings
        {
            get
            {
                return (StorageConnectionStringConfigurationElementCollection)this["connectionStrings"];
            }
        }
    }

    public class StorageConnectionStringConfigurationElement : NamedConfigurationElement
    {
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return (string)this["connectionString"];
            }
        }
    }

    public class StorageConnectionStringConfigurationElementCollection : NamedConfigurationElementCollection<StorageConnectionStringConfigurationElement>
    {
    }
}
