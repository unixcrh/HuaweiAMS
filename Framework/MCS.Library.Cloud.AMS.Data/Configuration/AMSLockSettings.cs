using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Configuration
{
    public class AMSLockSettings : DeluxeConfigurationSection
    {
        public static AMSLockSettings GetConfig()
        {
            AMSLockSettings result = (AMSLockSettings)ConfigurationBroker.GetSection("amsLockSettings");

            if (result == null)
                result = new AMSLockSettings();

            return result;
        }

        /// <summary>
		/// 是否启用锁机制
		/// </summary>
		[ConfigurationProperty("enabled", IsRequired = false, DefaultValue = "true")]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
        }

        /// <summary>
        /// 默认有效时间，单位是秒。如果上一次锁的检查时间超过此有效时间，则表明已经超时
        /// </summary>
        [ConfigurationProperty("defaultEffectiveTime", IsRequired = true, DefaultValue = "00:00:30")]
        public TimeSpan DefaultEffectiveTime
        {
            get
            {
                return (TimeSpan)this["defaultEffectiveTime"];
            }
        }
    }
}
