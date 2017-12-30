using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PASS.Common.Security
{
    public class SecurityService
    {
        string _iv;
        string _key;

        /// <summary>
        /// 初始化安全服務
        /// </summary>
        /// <param name="userNo">使用者編號</param>
        /// <param name="assignmentNo">作業編號</param>
        public SecurityService(string userNo="emptyIV-", string assignmentNo= "emptyKEY-")
        {
            //使用userNo,userID作為IV以及KEY
            _iv = assignmentNo.ToString().PadLeft(8, '0');
            _key = userNo.ToString().PadLeft(8, '0');

            _iv = GetRightString(_iv, 8);
            _key = GetRightString(_key, 8);
        }

        /// <summary>
        /// 將來源文字以雜湊運算後輸出
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

        /// <summary>
        /// 加密資料位元組
        /// </summary>
        /// <param name="dataByteArray"></param>
        /// <returns></returns>
        public byte[] EncryptFile(byte[] dataByteArray)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(_key);
            byte[] iv = Encoding.ASCII.GetBytes(_iv);

            des.Key = key;
            des.IV = iv;

            // 加密
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();

                return ms.ToArray();
            }
        }

        /// <summary>
        /// 解密資料位元組
        /// </summary>
        /// <param name="dataByteArray"></param>
        /// <returns></returns>
        public byte[] DecryptFile(byte[] dataByteArray)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] key = Encoding.ASCII.GetBytes(_key);
            byte[] iv = Encoding.ASCII.GetBytes(_iv);

            des.Key = key;
            des.IV = iv;

            // 解密
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();

                return ms.ToArray();
            }
        }

        private string GetRightString(string s, int length)
        {
            length = Math.Max(length, 0);

            if (s.Length > length)
            {
                return s.Substring(s.Length - length, length);
            }
            else
            {
                return s;
            }
        }
    }
}
