using MCS.Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.W3.Configuration
{
    public class W3IssuerConfigurationElement : NamedConfigurationElement
    {
        [ConfigurationProperty("privateCA")]
        public string PrivateCA
        {
            get
            {
                return (string)this["privateCA"];
            }
        }


        [ConfigurationProperty("issuerID")]
        public string IssuerID
        {
            get
            {
                return (string)this["issuerID"];
            }
        }

        [ConfigurationProperty("publicCA")]
        public string PublicCA
        {
            get
            {
                return (string)this["publicCA"];
            }
        }

        [ConfigurationProperty("privateCAPassword")]
        public string PrivateCAPassword
        {
            get
            {
                return (string)this["privateCAPassword"];
            }
        }

        [ConfigurationProperty("responseUri")]
        public string ResponseUri
        {
            get
            {
                return (string)this["responseUri"];
            }
        }
    }

    public class W3IssuerConfigurationElementCollection : NamedConfigurationElementCollection<W3IssuerConfigurationElement>
    {
    }
}
