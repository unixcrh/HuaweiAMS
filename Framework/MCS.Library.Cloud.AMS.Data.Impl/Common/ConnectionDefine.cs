using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data
{
    public static class ConnectionDefine
    {
        private const string DefaultDBConnectionName = "AMSDB";

        public static readonly DateTime MaxVersionEndTime = new DateTime(9999, 9, 9);

        public static string DBConnectionName
        {
            get
            {
                return DefaultDBConnectionName;
            }
        }
    }
}
