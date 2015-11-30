using MCS.Library.Cloud.AMS.DataObjects;
using MCS.Library.Core;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Mechanism
{
    public static class Converters
    {
        public static AMSChannelCollection ToAMSChannels(this IEnumerable<IChannel> channels)
        {
            channels.NullCheck("channels");

            AMSChannelCollection result = new AMSChannelCollection();

            channels.ForEach(c => result.Add(c.ToAMSChannel()));

            return result;
        }

        public static AMSChannel ToAMSChannel(this IChannel channel)
        {
            channel.NullCheck("channel");

            AMSChannel result = new AMSChannel();

            result.ID = channel.Id;
            result.Name = channel.Name;
            result.Description = channel.Description;
            result.State = channel.State.ToAMSChannelState();
            result.LastModified = channel.LastModified;

            return result;
        }

        public static AMSChannelState ToAMSChannelState(this ChannelState state)
        {
            return (AMSChannelState)((int)state);
        }
    }
}
