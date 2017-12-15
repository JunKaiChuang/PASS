using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PASS.Common.Security
{
    public class SecurityService
    {
        /// <summary>
        /// 將來源文字以雜湊運算
        /// </summary>
        /// <param name="plaintext">原始文字</param>
        /// <returns></returns>
        public string GetDigestText(string plaintext)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(plaintext);
            byte[] crypto = sha256.ComputeHash(source);
            string result = Convert.ToBase64String(crypto);
            return result;
        }

        /// <summary>
        /// 驗證來源文字與雜湊字串是否對應
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="digest"></param>
        /// <returns></returns>
        public bool VerifyText(string plaintext, string digest)
        {
            if (digest == GetDigestText(plaintext)) return true;
            return false;
        }
    }
}
