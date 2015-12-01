using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Configuration
{
    public class AMSDataAdapterSettings : ConfigurationSection
    {
        public static AMSDataAdapterSettings GetConfig()
        {
            AMSDataAdapterSettings settings = (AMSDataAdapterSettings)ConfigurationBroker.GetSection("amsDataAdapterSettings");

            settings.CheckSectionNotNull("amsDataAdapterSettings");

            return settings;
        }

        [ConfigurationProperty("typeFactories", IsRequired = true)]
        public TypeConfigurationCollection TypeFactories
        {
            get
            {
                return (TypeConfigurationCollection)this["typeFactories"];
            }
        }
    }
}
