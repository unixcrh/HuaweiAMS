﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SamlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(DESHelper.DecryptString(DESHelper.EncryptedPassword));
            X509Certificate2 cert = CertificateHelper.GetCertificate(".\\certificates\\HuaweiCA.p12", "Pr0d1234");

            Console.WriteLine(cert.ToString());
            //Console.WriteLine(CertificateHelper.GetPrivateKey(".\\certificates\\HuaweiCA.p12", "Pr0d1234").ToXmlString(false));

            SignXml();
        }

        private static XmlDocument SignXml()
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(".\\certificates\\samlRequestTemplate.xml");
            X509Certificate2 certificate = CertificateHelper.GetCertificate(".\\certificates\\HuaweiCA.p12", "Pr0d1234");
            AsymmetricAlgorithm key = certificate.PrivateKey;

            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            ns.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            XmlElement issuerNode = (XmlElement)xmlDoc.DocumentElement.SelectSingleNode("saml:Issuer", ns);

            SignedXml signedXml = new SignedXml(xmlDoc.DocumentElement);
            signedXml.SigningKey = key;
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

            KeyInfo keyInfo = new KeyInfo();

            XmlDocument keyDoc = new XmlDocument();

            keyDoc.LoadXml(certificate.PublicKey.Key.ToXmlString(false));
            keyInfo.LoadXml(keyDoc.DocumentElement);
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

            //xmlDoc.NameTable.Add("samlp");
            //XmlElement nameIDPolicyElem = xmlDoc.CreateElement("samlp", "NameIDPolicy", "urn:oasis:names:tc:SAML:2.0:protocol");
            //nameIDPolicyElem.SetAttribute("AllowCreate", "False");

            //xmlDoc.DocumentElement.AppendChild(nameIDPolicyElem);

            xmlDoc.Save("samleRequestCSharp.xml");

            return xmlDoc;
        }
    }
}
