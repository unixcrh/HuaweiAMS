using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    public static class OutputHelper
    {
        public static void Output(this AMSChannel channel)
        {
            if (channel != null)
            {
                Console.WriteLine("ID: {0}", channel.ID);
                Console.WriteLine("Name: {0}", channel.Name);
                Console.WriteLine("AMSAccountName: {0}", channel.AMSAccountName);
                Console.WriteLine("AMSID: {0}", channel.AMSID);

                if (channel is AMSChannelInEvent)
                { 
                    Console.WriteLine("IsDefault: {0}", ((AMSChannelInEvent)channel).IsDefault);
                    Console.WriteLine("DefaultPlaybackUrl: {0}", ((AMSChannelInEvent)channel).DefaultPlaybackUrl);
                    Console.WriteLine("CDNPlaybackUrl: {0}", ((AMSChannelInEvent)channel).CDNPlaybackUrl);
                }
            }
        }

        public static void Output(this IEnumerable<AMSChannel> channels)
        {
            if (channels != null)
                channels.ForEach(c => c.Output());
        }
    }
}
