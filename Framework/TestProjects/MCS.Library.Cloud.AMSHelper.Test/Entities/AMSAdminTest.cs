using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    [TestClass]
    public class AMSAdminTest
    {
        [TestMethod]
        public void CheckPasswordTest()
        {
            string logonName = "zhshen@microsoft.com";

            AMSAdmin user = AMSAdminSqlAdapter.Instance.CheckPassword(logonName, "password");

            Assert.IsNotNull(user);
            Assert.AreEqual(logonName, user.LogonName);
        }

        [TestMethod]
        public void SetPasswordTest()
        {
            string logonName = "ronchen@microsoft.com";

            AMSAdminSqlAdapter.Instance.SetPassword(logonName, "7412369");

            AMSAdmin user = AMSAdminSqlAdapter.Instance.CheckPassword(logonName, "7412369");

            Assert.IsNotNull(user);
            Assert.AreEqual(logonName, user.LogonName);
        }
    }
}
