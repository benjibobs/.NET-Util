using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace $privateproject
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptionUtil
    {
        public static string keystring = string.Empty;
        private static byte[] key = getHashSha256Bytes(keystring);
        private static byte[] iv = getIV(key);
        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static byte[] getHashSha256Bytes(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash;
        }

        public static byte[] getIV(byte[] key)
        {

           //Skipped for privacy reasons!
           return genericByteArray;
           
        }

        public static string encrypt(this string text)
        {
            SymmetricAlgorithm algorithm = Aes.Create();
            algorithm.Mode = CipherMode.ECB;
            algorithm.KeySize = 256;
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = Aes.Create();
            algorithm.Mode = CipherMode.ECB;
            algorithm.KeySize = 256;
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Encoding.UTF8.GetBytes("test");
            try
            {
                inputbuffer = Convert.FromBase64String(text);
            }catch(FormatException e){
                return "The text you provided is not in a recognised encrypted format";
            }
            try
            {
                byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
                return Encoding.Unicode.GetString(outputBuffer);
            }catch(Exception e){
                return "Invalid provided, could not decrypt text!";
            }
        }
    }
}
