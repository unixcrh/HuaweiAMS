using MCS.Library.Configuration;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Configuration
{
    public class AMSWorkerDurationConfigurationElement : NamedConfigurationElement
    {
        [ConfigurationProperty("duration", IsRequired = false, DefaultValue = "00:05:00")]
        public TimeSpan Duration
        {
            get
            {
                return (TimeSpan)this["duration"];
            }
        }
    }

    public class AMSWorkerDurationConfigurationElementCollection : NamedConfigurationElementCollection<AMSWorkerDurationConfigurationElement>
    {
        public TimeSpan GetDuration(string name, TimeSpan defaultDuration)
        {
            TimeSpan result = defaultDuration;

            if (this.ContainsKey(name))
                result = this[name].Duration;

            return result;
        }
    }
}
