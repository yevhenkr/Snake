// RZ.Crypto 1.0
//
// Шифрование данных.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace RZ
{
    public static class Crypto
    {

        public static readonly Encoding defaultEncoding = new UTF8Encoding();


        //MD5 из строки UTF8:
        public static string MD5(string strToEncrypt)
        {
            return MD5(strToEncrypt, defaultEncoding);
        }


        //MD5 из строки с указанием кодировки:
        public static string MD5(string strToEncrypt, Encoding e)
        {
            byte[] bytes = e.GetBytes(strToEncrypt);

            // encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";
            for (int i = 0; i < hashBytes.Length; i++)
            { hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0'); }
            return hashString.PadLeft(32, '0');
        }


        public const string HASH_CHARACTERS_FULL =
            "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public const string HASH_CHARACTERS_NORMAL =
            "0123456789abcdefghijklmnopqrstuvwx";


        // Random Hash:
        public static string GetRangomHash(
            int length = 10, string characters = HASH_CHARACTERS_NORMAL)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int r = Random.Range(0, characters.Length);
                sb.Append(characters[r]);
            }
            string code = sb.ToString();
            sb.Clear();
            return code;
        }
    }
}

