using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ECommerceProject.Infrastructure.Services
{
    public static class EncryptionHelper
    {
        public static string Encrypt(string plainText, string key)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.GenerateIV(); // random başlangıç noktası (başlatma vektörü)

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream ms = new();
            // başlangıç noktasından başla
            ms.Write(aes.IV, 0, aes.IV.Length);
            using CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
            using StreamWriter sw = new(cs);
            sw.Write(plainText);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText, string key)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);

            byte[] iv = new byte[16];
            Array.Copy(buffer, 0, iv, 0, iv.Length);
            aes.IV = iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            // kalan veriyi çözmek için akışı başlatıyoruz
            using MemoryStream ms = new(buffer, iv.Length, buffer.Length -  iv.Length);
            using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new(cs);

            return sr.ReadToEnd();
        }
    }
}
