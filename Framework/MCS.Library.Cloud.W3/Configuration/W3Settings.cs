using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.W3.Configuration
{
    public class W3Settings : ConfigurationSection
    {
        public static W3Settings GetSettings()
        {
            W3Settings settings = (W3Settings)ConfigurationBroker.GetSection("w3Settings");

            settings.CheckSectionNotNull("w3Settings");

            return settings;
        }

        [ConfigurationProperty("selectedIssuer")]
        public string SelectedIssuer
        {
            get
            {
                return (string)this["selectedIssuer"];
            }
        }

        [ConfigurationProperty("issuers")]
        public W3IssuerConfigurationElementCollection Issuers
        {
            get
            {
                return (W3IssuerConfigurationElementCollection)this["issuers"];
            }
        }
    }
}
