using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SamlTest
{
    public static class CertificateHelper
    {
        public static X509Certificate2 GetCertificate(string path, string password)
        {
            return new X509Certificate2(path, password);
        }

        public static AsymmetricAlgorithm GetPrivateKey(string path, string password)
        {
            X509Certificate2 cert = CertificateHelper.GetCertificate(path, password);

            return cert.PrivateKey;
        }

        public static X509Certificate2 GetCertificate(string path)
        {
            return new X509Certificate2(path);
        }
    }
}
