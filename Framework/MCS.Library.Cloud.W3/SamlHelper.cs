using MCS.Library.Cloud.W3.Configuration;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static string CheckAndGetUserIDResponseDoc(XmlDocument xmlDoc)
        {
            bool validateResult = false;

            string userID = ValidateAndGetUserIDResponseDoc(xmlDoc, out validateResult);

            validateResult.FalseThrow("W3认证返回的结果验证不通过");

            return userID;
        }

        public static string ValidateAndGetUserIDResponseDoc(XmlDocument xmlDoc, out bool validateResult)
        {
            xmlDoc.NullCheck("xmlDoc");

            validateResult = false;
            string userID = string.Empty;

            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            ns.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
            ns.AddNamespace("x", "http://www.w3.org/2000/09/xmldsig#");

            XmlElement signatureElem = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("//x:Signature", ns);

            if (signatureElem != null)
            {
                XmlElement assertionNode = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("saml:Assertion", ns);

                if (assertionNode != null)
                {
                    SignedXml signedXml = new SignedXml(assertionNode);

                    signedXml.LoadXml(signatureElem);

                    X509Certificate2 certificate = GetEmbededPublicCertificate();

                    validateResult = signedXml.CheckSignature(certificate, true);

                    userID = assertionNode.GetSingleNodeText("saml:Subject/saml:NameID", ns);
                }
            }

            return userID;
        }

        public static XmlDocument GetSignedRequestDoc(string issuer, string assertionUrl)
        {
            issuer.CheckStringIsNullOrEmpty("issuer");

            XmlDocument xmlDoc = Assembly.GetExecutingAssembly().LoadXmlFromResource("MCS.Library.Cloud.W3.Resources.samlRequestTemplate.xml");

            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            ns.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            FillPrameters(xmlDoc, ns, issuer, assertionUrl);
            AddSignatureNode(xmlDoc, ns, GetEmbededPrivateCertificate());

            return xmlDoc;
        }

        private static void FillPrameters(XmlDocument xmlDoc, XmlNamespaceManager ns, string issuer, string assertionUrl)
        {
            xmlDoc.DocumentElement.SetAttribute("ID", "_" + UuidHelper.NewUuidString());

            if (assertionUrl.IsNullOrEmpty())
                assertionUrl = W3Settings.GetSettings().GetSelectedIssuer().ResponseUri;

            xmlDoc.DocumentElement.SetAttribute("AssertionConsumerServiceURL", assertionUrl);

            XmlElement issuerNode = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("saml:Issuer", ns);

            if (issuerNode != null)
                issuerNode.InnerText = issuer;
        }

        private static void AddSignatureNode(XmlDocument xmlDoc, XmlNamespaceManager ns, X509Certificate2 certificate)
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

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MCS.Library.Cloud.W3.Resources." +
                W3Settings.GetSettings().GetSelectedIssuer().PrivateCA))
            {
                rawData = stream.ToBytes();
            }

            return new X509Certificate2(rawData, "Pr0d1234");
        }

        private static X509Certificate2 GetEmbededPublicCertificate()
        {
            byte[] rawData = null;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MCS.Library.Cloud.W3.Resources." +
                W3Settings.GetSettings().GetSelectedIssuer().PublicCA))
            {
                rawData = stream.ToBytes();
            }

            return new X509Certificate2(rawData);
        }
    }
}
