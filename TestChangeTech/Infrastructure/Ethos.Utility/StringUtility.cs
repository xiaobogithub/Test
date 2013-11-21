using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Ethos.Utility
{
    public class StringUtility
    {
        /// <summary>
        ///  Md5 Encrypt
        /// </summary>
        /// <param name="pToEncrypt">Encrypt string</param>
        /// <param name="sKey">Encrypt key</param>
        /// <returns></returns>
        public static string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write); cs.Write(inputByteArray, 0, inputByteArray.Length); cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach(byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// Md5 Decrypt
        /// </summary>
        /// <param name="pToEncrypt">Decrypt string</param>
        /// <param name="sKey">Decrypt key(must be 8 charameter)</param>
        /// <returns></returns>
        public static string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for(int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        //public static string RemovePortNumber(string url)
        //{
        //    string returnURL = string.Empty;

        //    if (url.Contains(':'))
        //    {
        //        string[] partsOfURL = url.Split(':');
        //        string webRoot = partsOfURL[0];
        //        string pageAddress = partsOfURL[1].Substring(partsOfURL[1].IndexOf('/'));

        //        returnURL = webRoot + pageAddress;
        //    }
        //    else
        //    {
        //        returnURL = url;
        //    }

        //    return returnURL;
        //}

        public static string RemoveAzurePort(string url)
        {
            if(url.Contains(":20000"))
            {
                url = url.Remove(url.IndexOf(":20000"), 6);
            }

            return url;
        }

        public static string GenerateCheckCode(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.UtcNow.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for(int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        public static string GenerateCheckLetterCode(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "X";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }

    }
}
