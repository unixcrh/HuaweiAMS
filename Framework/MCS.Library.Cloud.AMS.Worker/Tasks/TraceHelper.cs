using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Tasks
{
    public static class TraceHelper
    {
        internal static TraceSource AMSTaskTraceSource = new TraceSource("amsTaskTraceSource");

        public static string ToTraceInfo(this AMSChannel channel)
        {
            StringBuilder strB = new StringBuilder();

            StringWriter writer = new StringWriter(strB);

            writer.WriteLine("ID: {0}", channel.ID);
            writer.WriteLine("AccountName: {0}", channel.AMSAccountName);
            writer.WriteLine("Channel ID: {0}", channel.AMSID);
            writer.WriteLine("Name: {0}", channel.Name);
            writer.WriteLine("State: {0}", channel.State);
            writer.WriteLine("LastModified: {0:yyyy-MM-dd HH:mm:ss}", channel.AMSLastModified);

            return strB.ToString();
        }

        public static string ToTraceInfo(this AMSEvent eventData)
        {
            StringBuilder strB = new StringBuilder();

            StringWriter writer = new StringWriter(strB);

            writer.WriteLine("ID: {0}", eventData.ID);
            writer.WriteLine("Channel ID: {0}", eventData.ChannelID);
            writer.WriteLine("Name: {0}", eventData.Name);
            writer.WriteLine("AMS Program ID", eventData.AMSProgramID);
            writer.WriteLine("State: {0}", eventData.State);

            return strB.ToString();
        }
    }
}
