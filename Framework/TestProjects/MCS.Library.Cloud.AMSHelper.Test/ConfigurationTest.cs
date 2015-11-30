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

            OutputSettings(settings);
        }

        private static void OutputSettings(MediaServiceAccountSettings settings)
        {
            foreach (MediaServiceAccountConfigurationElement element in settings.Accounts)
            {
                Console.WriteLine("Account Name: {0}, Account Key: {1}",
                    element.AccountName, element.AccountKey);
            }
        }
    }
}
