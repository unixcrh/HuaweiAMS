using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SamlTest
{
    static class DESHelper
    {
        /// <summary>
        /// 实际密码为 Pr0d1234
        /// </summary>
        public const string EncryptedPassword = "voaLwJNx4MPJGdZ9lmezow==";
        public const string EncryptedKey = "voaLwJNx4MPJGdZ9lmezow==";

        private const string DesKey = "key4kerb";

        public static string DecryptString(string strData)
        {
            byte[] inputByteArray = Convert.FromBase64String(strData);
            string result = string.Empty;

            using (DESCryptoServiceProvider provider = new DESCryptoServiceProvider())
            {
                provider.Key = Encoding.UTF8.GetBytes(DesKey);
                provider.IV = Encoding.UTF8.GetBytes(DesKey);
                provider.Mode = CipherMode.ECB;

                using (MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, provider.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cs.Close();

                        result = Encoding.Default.GetString(ms.ToArray());
                    }
                }

                return result;
            }
        }
    }
}
