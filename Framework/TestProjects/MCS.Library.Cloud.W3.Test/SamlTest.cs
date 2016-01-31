using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

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
    }
}
