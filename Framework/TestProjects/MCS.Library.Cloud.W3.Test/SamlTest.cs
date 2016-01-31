using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using MCS.Library.Core;
using System.Reflection;

namespace MCS.Library.Cloud.W3.Test
{
    [TestClass]
    public class SamlTest
    {
        [TestMethod]
        public void SamlRequestTest()
        {
            XmlDocument xmlDoc = SamlHelper.GetSignedRequestDoc("www.Kenexa.com", string.Empty);

            Console.WriteLine(xmlDoc.OuterXml);
        }

        [TestMethod]
        public void SamlResponseTest()
        {
            string responseString = ResourceHelper.LoadStringFromResource(Assembly.GetExecutingAssembly(), "MCS.Library.Cloud.W3.Test.Resources.samlResponse.xml");

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(responseString);

            Console.WriteLine(SamlHelper.ValidateResponseDoc(xmlDoc));
        }
    }
}
