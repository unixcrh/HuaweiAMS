using MCS.Library.Cloud.AMS.Data.Contracts;
using MCS.Library.Cloud.AMS.Data.Entities;
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

            channels.AsEnumerable().ForEach(c => result.Add(c.ToAMSChannel()));

            return result;
        }

        public static AMSChannel ToAMSChannel(this IChannel channel)
        {
            channel.NullCheck("channel");

            AMSChannel result = new AMSChannel();

            result.ID = channel.Id;

            channel.FillAMSChannel(result);

            return result;
        }

        public static void FillAMSChannel(this IChannel channel, AMSChannel result)
        {
            channel.NullCheck("channel");
            result.NullCheck("result");

            result.AMSID = channel.Id;
            result.Name = channel.Name;
            result.Description = channel.Description;
            result.State = channel.State.ToAMSChannelState();
            result.AMSLastModified = channel.LastModified;

            if (channel.Preview != null)
                result.PreviewUrl = channel.Preview.Endpoints.GetDefaultUrl();

            if (channel.Input != null)
            {
                result.PrimaryInputUrl = channel.Input.Endpoints.GetDefaultUrl();
                result.SecondaryInputUrl = channel.Input.Endpoints.GetSecondaryUrl();
            }
        }

        public static void FillAMSEvent(this IProgram program, AMSChannel channel, IProgramRelativeEntity eventData)
        {
            if (program != null)
            {
                eventData.AMSProgramID = program.Id;
                eventData.State = program.State.ToAMSEventState();

                IAsset asset = program.Asset;

                if (asset != null)
                {
                    IAssetFile file = asset.AssetFiles.AsEnumerable().FirstOrDefault(a => a.Name.EndsWith(".ism"));

                    if (file != null)
                    {
                        ILocator locator = asset.Locators.FirstOrDefault();

                        if (locator != null)
                        {
                            eventData.DefaultPlaybackUrl = locator.Path + file.Name + "/manifest";
                            eventData.CDNPlaybackUrl = GetCDNPlaybackUrl(locator.Path, eventData.DefaultPlaybackUrl, file.Name, channel.CDNPrefixMode, channel.CDNPrefix);
                        }
                    }
                }
            }
        }

        public static AMSChannelState ToAMSChannelState(this ChannelState state)
        {
            return (AMSChannelState)((int)state);
        }

        public static AMSEventState ToAMSEventState(this ProgramState state)
        {
            return (AMSEventState)((int)state);
        }

        private static string GetCDNPlaybackUrl(string locatorPath, string defaultPlaybackUrl, string fileName, AMSCDNPrefixMode prefixMode, string cdnPrefix)
        {
            Uri locatorUri = new Uri(locatorPath);

            string result = defaultPlaybackUrl;

            switch (prefixMode)
            {
                case AMSCDNPrefixMode.Prefix:
                    result = MergePlaybackCDNPrefix(locatorUri, cdnPrefix, fileName);
                    break;
                case AMSCDNPrefixMode.Host:
                    result = MergePlaybackCDNHost(locatorUri, cdnPrefix, fileName);
                    break;
            }

            return result;
        }

        private static string MergePlaybackCDNHost(Uri locatorPath, string cdnHost, string fileName)
        {
            string locatorHost = locatorPath.Host + (locatorPath.Port == 80 ? string.Empty : ":" + locatorPath.Port);

            return locatorPath.Scheme + "://" + (cdnHost ?? locatorHost) + locatorPath.PathAndQuery + fileName + "/manifest";
        }

        private static string MergePlaybackCDNPrefix(Uri locatorPath, string cdnPrefix, string fileName)
        {
            return locatorPath.Scheme + "://" + (cdnPrefix ?? string.Empty) + locatorPath.Host + (locatorPath.Port == 80 ? string.Empty : ":" + locatorPath.Port) +
                                locatorPath.PathAndQuery + fileName + "/manifest";
        }

        private static string GetDefaultUrl(this IEnumerable<ChannelEndpoint> endpoints)
        {
            string result = string.Empty;

            ChannelEndpoint endpoint = endpoints.FirstOrDefault();

            if (endpoint != null && endpoint.Url != null)
                result = endpoint.Url.ToString();

            return result;
        }

        private static string GetSecondaryUrl(this IEnumerable<ChannelEndpoint> endpoints)
        {
            string result = string.Empty;

            int index = 0;

            foreach (ChannelEndpoint endpoint in endpoints.AsEnumerable())
            {
                if (index == 1)
                {
                    if (endpoint != null && endpoint.Url != null)
                        result = endpoint.Url.ToString();

                    break;
                }

                index++;
            }

            return result;
        }
    }
}
