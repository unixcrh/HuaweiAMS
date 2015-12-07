using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Tasks
{
    public static class TraceHelper
    {
        public static string ToTraceInfo(this AMSChannel channel)
        {
            StringBuilder strB = new StringBuilder();

            StringWriter writer = new StringWriter(strB);

            writer.WriteLine("AccountName: {0}", channel.AMSAccountName);
            writer.WriteLine("Channel ID: {0}", channel.AMSID);
            writer.WriteLine("Name: {0}", channel.Name);
            writer.WriteLine("State: {0}", channel.State);
            writer.WriteLine("LastModified: {0:yyyy-MM-dd HH:mm:ss}", channel.AMSLastModified);

            return strB.ToString();
        }
    }
}
