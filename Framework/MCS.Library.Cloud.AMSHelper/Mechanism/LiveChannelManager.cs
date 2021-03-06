﻿using MCS.Library.Cloud.AMS.Data.Contracts;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Data.Mechanism;
using MCS.Library.Cloud.AMSHelper.Configuration;
using MCS.Library.Core;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Mechanism
{
    public static class LiveChannelManager
    {
        internal static TraceSource AMSOpTraceSource = new TraceSource("amsOpTraceSource");

        /// <summary>
        /// 获取所有的频道信息。从数据库中读取，同时和配置文件中的频道合并。
        /// 配置文件中不存在的，则标志为Disabled。
        /// </summary>
        /// <param name="mergeInfoFromCloud">是否从云端同步状态</param>
        /// <returns></returns>
        public static AMSChannelCollection GetAllChannels(bool mergeInfoFromCloud = false)
        {
            AMSChannelCollection channelsInDB = ContractManager.GetAMSChannelAdapter().GetAllChannels();
            //AMSChannelCollection channelsFromConfig = GetChannelsFromConfig();

            //if (mergeInfoFromCloud)
            //    FillChannelsInfoFromCloud(channelsFromConfig);
            if (mergeInfoFromCloud)
                FillChannelsInfoFromCloud(channelsInDB);

            //return channelsInDB.MergeFrom(channelsFromConfig);
            return channelsInDB.MergeFrom(channelsInDB);
        }

        public static void StartChannel(AMSChannel channel)
        {
            if (channel != null && channel.AMSID.IsNotEmpty())
            {
                CloudMediaContext context = MediaServiceAccountSettings.GetConfig().Accounts.GetCloudMediaContext(channel.AMSAccountName);

                IChannel amsChannel = GetChannelByID(context, channel.AMSID);

                if (amsChannel != null)
                {
                    if (amsChannel.State == ChannelState.Stopped)
                        TraceOperation("Channel {0}", () => amsChannel.Start(), amsChannel.Name);

                    amsChannel.FillAMSChannel(channel);
                }
            }
        }

        public static void StartProgram(AMSChannel channel, AMSEvent eventData, IProgramRelativeEntity retProgramInfo)
        {
            if (channel != null && channel.AMSID.IsNotEmpty())
            {
                CloudMediaContext context = MediaServiceAccountSettings.GetConfig().Accounts.GetCloudMediaContext(channel.AMSAccountName);

                IChannel amsChannel = GetChannelByID(context, channel.AMSID);

                if (amsChannel != null)
                {
                    IProgram program = GetProgramByEvent(amsChannel, eventData);

                    if (program == null)
                        program = CreateProgram(context, amsChannel, eventData);

                    if (channel.State == AMSChannelState.Running && program.State == ProgramState.Stopped)
                        TraceOperation("Start Program {0}", () => program.Start(), program.Name);

                    program.FillAMSEvent(channel, retProgramInfo);
                }
            }
        }

        public static void StopProgram(AMSChannel channel, AMSEvent eventData, IProgramRelativeEntity retProgramInfo)
        {
            if (channel != null && channel.AMSID.IsNotEmpty())
            {
                CloudMediaContext context = MediaServiceAccountSettings.GetConfig().Accounts.GetCloudMediaContext(channel.AMSAccountName);

                IChannel amsChannel = GetChannelByID(context, channel.AMSID);

                if (amsChannel != null)
                {
                    IProgram program = GetProgramByEvent(amsChannel, eventData);

                    if (program != null)
                    {
                        if (program.State == ProgramState.Running)
                            TraceOperation("Stop Program {0}", () => program.Stop(), program.Name);

                        program.FillAMSEvent(channel, retProgramInfo);

                        if (retProgramInfo.State == AMSEventState.NotStart)
                            retProgramInfo.State = AMSEventState.Completed;
                    }
                    else
                        retProgramInfo.State = AMSEventState.Completed;
                }
            }
        }

        public static int DeleteAllExpiredPrograms(TimeSpan expiredTime)
        {
            int count = 0;

            AMSChannelCollection channels = GetAllChannels(false);

            foreach (AMSChannel channel in channels)
            {
                CloudMediaContext context = MediaServiceAccountSettings.GetConfig().Accounts.GetCloudMediaContext(channel.AMSAccountName);

                IChannel amsChannel = GetChannelByID(context, channel.AMSID);

                if (amsChannel != null)
                {
                    foreach (IProgram program in amsChannel.Programs.ToArray())
                    {
                        if (program.State == ProgramState.Stopped && (program.LastModified + expiredTime) < DateTime.UtcNow)
                        {
                            TraceOperation("Delete Program {0}", () => program.Delete(), program.Name);
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public static void StopChannel(AMSChannel channel)
        {
            if (channel != null && channel.AMSID.IsNotEmpty())
            {
                CloudMediaContext context = MediaServiceAccountSettings.GetConfig().Accounts.GetCloudMediaContext(channel.AMSAccountName);

                IChannel amsChannel = GetChannelByID(context, channel.AMSID);

                if (amsChannel != null)
                {
                    if (amsChannel.State == ChannelState.Running)
                        TraceOperation("Stop Channel {0}", () => amsChannel.Stop(), amsChannel.Name);

                    amsChannel.FillAMSChannel(channel);
                }
            }
        }

        private static IProgram CreateProgram(CloudMediaContext context, IChannel amsChannel, AMSEvent eventData)
        {
            string assetName = string.Format("{0}-{1}", amsChannel.Name, GetProgramName(eventData));

            IAsset asset = GetAssetByName(context, assetName);

            if (asset == null)
                asset = TraceOperation("Create " + assetName, () => CreateAsset(context, assetName));

            if (asset.Locators.Count == 0)
                TraceOperation("Create Locator", () => CreateLocator(context, asset));

            ProgramCreationOptions options = new ProgramCreationOptions();

            options.Name = GetProgramName(eventData);
            options.AssetId = asset.Id;
            options.Description = eventData.Name;
            options.ArchiveWindowLength = TimeSpan.FromHours(4);

            return TraceOperation("Create Program {0}", () => amsChannel.Programs.Create(options), options.Name);
        }

        private static void CreateLocator(CloudMediaContext context, IAsset asset)
        {
            IAccessPolicy policy = GetOrCreateAccessPolicy(context, asset.Name);

            context.Locators.CreateLocator(LocatorType.OnDemandOrigin, asset, policy);
        }

        //private const string DefaultPolicyName = "ReadOnly3000Days";

        private static IAccessPolicy GetOrCreateAccessPolicy(CloudMediaContext context, string assetName)
        {
            //IAccessPolicy result = context.AccessPolicies.Where(p => p.Name == DefaultPolicyName).FirstOrDefault();

            //if (result == null)
            //    result = context.AccessPolicies.Create(DefaultPolicyName, TimeSpan.FromDays(3000), AccessPermissions.Read);

            return context.AccessPolicies.Create("AP:" + assetName, TimeSpan.FromDays(3000), AccessPermissions.Read);
        }

        private static IAsset CreateAsset(CloudMediaContext context, string assetName)
        {
            return context.Assets.Create(assetName, AssetCreationOptions.None);
        }

        private static IAsset GetAssetByName(CloudMediaContext context, string assetName)
        {
            return context.Assets.Where(a => a.Name == assetName).FirstOrDefault();
        }

        private static IChannel GetChannelByID(CloudMediaContext context, string channelID)
        {
            return context.Channels.Where(c => c.Id == channelID).FirstOrDefault();
        }

        private static IProgram GetProgramByEvent(IChannel channel, AMSEvent eventData)
        {
            IProgram result = null;

            if (channel != null)
                result = channel.Programs.Where(p => p.Name == GetProgramName(eventData)).FirstOrDefault();

            return result;
        }

        private static string GetProgramName(AMSEvent eventData)
        {
            return string.Format("Event-{0}", eventData.ID);
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

        private static void TraceOperation(string opName, Action action, params object[] args)
        {
            string operation = string.Format(opName, args);

            AMSOpTraceSource.TraceEvent(TraceEventType.Information, 61000, "Start: {0}", operation);

            action();

            AMSOpTraceSource.TraceEvent(TraceEventType.Information, 61000, "Complete: {0}", operation);
        }

        private static T TraceOperation<T>(string opName, Func<T> func, params object[] args)
        {
            string operation = string.Format(opName, args);

            AMSOpTraceSource.TraceEvent(TraceEventType.Information, 61000, "Start: {0}", operation);

            T result = func();

            AMSOpTraceSource.TraceEvent(TraceEventType.Information, 61000, "Complete: {0}", operation);

            return result;
        }
    }
}
