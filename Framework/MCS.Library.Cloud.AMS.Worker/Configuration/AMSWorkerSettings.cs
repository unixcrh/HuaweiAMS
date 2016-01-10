using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Configuration
{
    public class AMSWorkerSettings : DeluxeConfigurationSection
    {
        public static AMSWorkerSettings GetConfig()
        {
            AMSWorkerSettings settings = (AMSWorkerSettings)ConfigurationBroker.GetSection("amsWorkerSettings");

            if (settings == null)
                settings = new AMSWorkerSettings();

            return settings;
        }

        [ConfigurationProperty("defaultWarmupTime", IsRequired = false, DefaultValue = "00:05:00")]
        public TimeSpan DefaultWarmupTime
        {
            get
            {
                return (TimeSpan)this["defaultWarmupTime"];
            }
        }

        [ConfigurationProperty("itemTypes", IsRequired = false)]
        public AMSQueueItemTypeConfigurationElementCollection ItemTypes
        {
            get
            {
                return (AMSQueueItemTypeConfigurationElementCollection)this["itemTypes"];
            }
        }

        [ConfigurationProperty("enableSimulation", DefaultValue = false, IsRequired = false)]
        public bool EnableSimulation
        {
            get
            {
                return (bool)this["enableSimulation"];
            }
        }

        [ConfigurationProperty("durations", IsRequired = false)]
        public AMSWorkerDurationConfigurationElementCollection Durations
        {
            get
            {
                return (AMSWorkerDurationConfigurationElementCollection)this["durations"];
            }
        }
    }
}
