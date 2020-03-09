using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MakerSpaceMK2.Services
{
    public static class PasswordServices
    {

        public static byte[] SaltGen()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }


        public static string HashGen(byte[] salt, string plainTextPass)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(plainTextPass, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
        }

        public static bool CompareHash(string hashOne, string hashTwo)
        {
            return hashOne.Equals(hashTwo);
        }
    }
}