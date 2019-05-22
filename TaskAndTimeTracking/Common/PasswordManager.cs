using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TaskAndTimeTracking.Common
{
    public class PasswordManager 
    {
        
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(salt);
            }

            return salt;
        }

        public static string GeneratePasswordHash(byte[] salt, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, salt, KeyDerivationPrf.HMACSHA256, 10000,32));
        }
        
    }
}