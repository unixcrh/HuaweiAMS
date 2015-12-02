using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Data.Mechanism;
using MCS.Library.Cloud.AMSHelper.Configuration;
using MCS.Library.Core;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Mechanism
{
    public static class LiveChannelManager
    {
        /// <summary>
        /// 获取所有的频道信息。从数据库中读取，同时和配置文件中的频道合并。
        /// 配置文件中不存在的，则标志为Disabled。
        /// </summary>
        /// <param name="mergeInfoFromCloud">是否从云端同步状态</param>
        /// <returns></returns>
        public static AMSChannelCollection GetAllChannels(bool mergeInfoFromCloud = false)
        {
            AMSChannelCollection channelsInDB = ContractManager.GetAMSChannelAdapter().GetAllChannels();
            AMSChannelCollection channelsFromConfig = GetChannelsFromConfig();

            if (mergeInfoFromCloud)
                FillChannelsInfoFromCloud(channelsFromConfig);

            return channelsInDB.MergeFrom(channelsFromConfig);
        }

        /// <summary>
        /// 从配置文件中读取频道信息
        /// </summary>
        /// <returns></returns>
        private static AMSChannelCollection GetChannelsFromConfig()
        {
            MediaServiceAccountSettings accountSettings = MediaServiceAccountSettings.GetConfig();
            AMSChannelCollection channels = new AMSChannelCollection();

            foreach (LiveChannelConfigurationElement channelElem in LiveChannelSettings.GetConfig().Channels)
            {
                if (accountSettings.Accounts.ContainsKey(channelElem.AccountName))
                {
                    AMSChannel channel = new AMSChannel();

                    channel.ID = UuidHelper.NewUuidString();
                    channel.Name = channelElem.ChannelName;
                    channel.Description = channelElem.Description;
                    channel.AMSAccountName = channelElem.AccountName;

                    channels.Add(channel);
                }
            }

            return channels;
        }

        private static void FillChannelsInfoFromCloud(AMSChannelCollection channels)
        {
            Dictionary<string, AMSChannelCollection> cloundAMSInfo = new Dictionary<string, AMSChannelCollection>(StringComparer.OrdinalIgnoreCase);

            foreach (AMSChannel target in channels)
            {
                AMSChannelCollection channelsInContext = null;

                if (cloundAMSInfo.TryGetValue(target.AMSAccountName, out channelsInContext) == false)
                {
                    CloudMediaContext context = MediaServiceAccountSettings.GetConfig().Accounts.GetCloudMediaContext(target.AMSAccountName);

                    if (context != null)
                    {
                        channelsInContext = context.Channels.ToAMSChannels();
                        cloundAMSInfo.Add(target.AMSAccountName, channelsInContext);
                    }
                }

                if (channelsInContext != null)
                {
                    AMSChannel channelInCloud = null;

                    if (target.AMSID.IsNotEmpty())
                        channelInCloud = channelsInContext.FindChannelByAMSID(target.AMSID);
                    else
                    if (target.Name.IsNotEmpty())
                        channelInCloud = channelsInContext.FindChannelByName(target.Name);

                    if (channelInCloud != null)
                        target.FillStatusFromCloud(channelInCloud);
                }
            }
        }
    }
}
