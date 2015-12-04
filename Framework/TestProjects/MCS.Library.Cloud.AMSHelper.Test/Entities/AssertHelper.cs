using MCS.Library.Cloud.AMS.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    public static class AssertHelper
    {
        public static void AreEqual(this AMSChannel expected, AMSChannel actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.State, actual.State);
        }

        public static void AreEqual(this AMSEvent expected, AMSEvent actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.ChannelID, actual.ChannelID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.State, actual.State);
        }

        public static void AreEqual(this UserOperationLog expected, UserOperationLog actual)
        {
            Assert.AreEqual(expected.ResourceID, actual.ResourceID);
            Assert.AreEqual(expected.Subject, actual.Subject);
        }
    }
}
