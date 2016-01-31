using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MCS.Library.Cloud.W3
{
    public static class SamlHelper
    {
        public static XmlDocument GetSignedRequestDoc(string issuer, string assertionUrl)
        {
            issuer.CheckStringIsNullOrEmpty("issuer");

            XmlDocument xmlDoc = Assembly.GetExecutingAssembly().LoadXmlFromResource("MCS.Library.Cloud.W3.Resources.samlRequestTemplate.xml");

            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            ns.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            FillPrameters(xmlDoc, ns, issuer, assertionUrl);
            AddSignatureNodce(xmlDoc, ns, GetEmbededPrivateCertificate());

            return xmlDoc;
        }

        private static void FillPrameters(XmlDocument xmlDoc, XmlNamespaceManager ns, string issuer, string assertionUrl)
        {
            xmlDoc.DocumentElement.SetAttribute("ID", "_" + UuidHelper.NewUuidString());

            if (assertionUrl.IsNotEmpty())
                xmlDoc.DocumentElement.SetAttribute("AssertionConsumerServiceURL", assertionUrl);
        }

        private static void AddSignatureNodce(XmlDocument xmlDoc, XmlNamespaceManager ns, X509Certificate2 certificate)
        {
            XmlElement issuerNode = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("saml:Issuer", ns);

            SignedXml signedXml = new SignedXml(xmlDoc.DocumentElement);
            signedXml.SigningKey = certificate.PrivateKey;
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

            KeyInfo keyInfo = new KeyInfo();

            keyInfo.AddClause(new KeyInfoX509Data(certificate));

            signedXml.KeyInfo = keyInfo;

            string refId = xmlDoc.DocumentElement.GetAttribute("ID");

            Reference reference = new Reference();
            reference.Uri = "#" + refId;

            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();

            reference.AddTransform(env);

            XmlDsigExcC14NTransform env2 = new XmlDsigExcC14NTransform();
            env2.InclusiveNamespacesPrefixList = "#default code ds kind rw saml samlp typens";
            reference.AddTransform(env2);

            signedXml.AddReference(reference);

            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.InsertAfter(xmlDoc.ImportNode(xmlDigitalSignature, true), issuerNode);
        }

        private static X509Certificate2 GetEmbededPrivateCertificate()
        {
            byte[] rawData = null;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MCS.Library.Cloud.W3.Resources.HuaweiCA.p12"))
            {
                rawData = stream.ToBytes();
            }

            return new X509Certificate2(rawData, "Pr0d1234");
        }
    }
}
