using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Configuration
{
    public class AMSQueueItemTypeConfigurationElement : NamedConfigurationElement
    {
        /// <summary>
		/// 是否启用该操作
		/// </summary>
		[ConfigurationProperty("enabled", IsRequired = false, DefaultValue = "true")]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
        }
    }

    public class AMSQueueItemTypeConfigurationElementCollection : NamedConfigurationElementCollection<AMSQueueItemTypeConfigurationElement>
    {
        public bool IsEnabled(AMSQueueItemType itemType)
        {
            bool result = false;

            if (this.ContainsKey(itemType.ToString()))
                result = this[itemType.ToString()].Enabled;

            return result;
        }
    }
}
