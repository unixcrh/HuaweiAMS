using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCS.Library.Cloud.AMSHelper.Configuration;

namespace MCS.Library.Cloud.AMSHelper.Test
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void ListConfigedAccounts()
        {
            MediaServiceAccountSettings settings = MediaServiceAccountSettings.GetConfig();

            OutputAccountSettings(settings);
        }

        [TestMethod]
        public void ListConfigedChannels()
        {
            LiveChannelSettings settings = LiveChannelSettings.GetConfig();

            OutputChannelSettings(settings);
        }

        private static void OutputChannelSettings(LiveChannelSettings settings)
        {
            foreach (LiveChannelConfigurationElement element in settings.Channels)
            {
                Console.WriteLine("Channel Name: {0}, Account Name: {1}",
                    element.ChannelName, element.AccountName);
            }
        }

        private static void OutputAccountSettings(MediaServiceAccountSettings settings)
        {
            foreach (MediaServiceAccountConfigurationElement element in settings.Accounts)
            {
                Console.WriteLine("Account Name: {0}, Account Key: {1}",
                    element.AccountName, element.AccountKey);
            }
        }
    }
}
