using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Util
{
    class DCEncrypt
    {
        public static string key
        {
            get
            {
                return "TVD1NJPRL6T2caV9NvLXQw==";
            }
        }
        /// <summary>
        /// 有密码的AES加密 
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            byte[] keyArray = Convert.FromBase64String(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return BitConverter.ToString(resultArray).Replace("-", "");
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            byte[] keyArray = Convert.FromBase64String(key);
            byte[] toEncryptArray = SplitLength(toDecrypt, 2);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
                /// 按长度分割字符串，汉字按一个字符算
                /// </summary>
                /// <param name="SourceString"></param>
                /// <param name="Length"></param>
                /// <returns></returns>
        public static byte[] SplitLength(string SourceString, int Length)
        {
            //List<string> DestString = new List<string>();
            List<string> list = new List<string>();
            for (int i = 0; i < SourceString.Trim().Length; i += Length)
            {
                if ((SourceString.Trim().Length - i) >= Length)
                    list.Add(SourceString.Trim().Substring(i, Length));
                else
                    list.Add(SourceString.Trim().Substring(i, SourceString.Trim().Length - i));
            }
            byte[] data = new byte[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                data[i] = Convert.ToByte(list[i],16);
            }
            return data;
        }
    }
}
