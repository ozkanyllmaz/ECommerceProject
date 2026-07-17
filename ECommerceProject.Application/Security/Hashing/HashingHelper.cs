using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceProject.Application.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Şifreyi Tuzla harmanla ve hashle
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // db deki tuzu algoritmaya veriyoruz ki aynı kalıpla şifreleme yapsın.
            using var hmac = new HMACSHA512(passwordSalt);

            // kullanıcının girdiği düz şifreyi, db deki tuz ile tekrar hashliyoruz.
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Ürettiğimiz hash ile db'den gelen hashi byte byte karşılaştırıyoruz.
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
