using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.W3
{
    public class SamlResponseResult
    {
        public string UserID
        {
            get;
            set;
        }

        public string ReturnUrl
        {
            get;
            set;
        }

        public bool ValidateResult
        {
            get;
            set;
        }
    }
}
