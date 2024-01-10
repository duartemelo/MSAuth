using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.DomainServices
{
    public class SharedUtilities
    {
        public struct Password
        {
            public string hash;
            public string salt;
        }

        public static Password GeneratePassword(string password)
        {
            string salt = GenerateSalt();
            string hash = GenerateHash(password, salt);
            return new Password
            {
                hash = hash,
                salt = salt
            };
        }

        public static string GenerateSalt()
        {
            byte[] salt;
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt = new byte[16]);
            return Convert.ToBase64String(salt);
        }

        public static string GenerateHash(string plainPassword, string salt)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(String.Concat(salt, plainPassword));
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(byteArray);
            return Convert.ToBase64String(hashedBytes);
        }

        public static bool ValidatePassword(string userInputPassword, string existingHashedBase64StringPassword, string existingSalt)
        {
            string UserInputHashedPassword = GenerateHash(userInputPassword, existingSalt);
            return existingHashedBase64StringPassword == UserInputHashedPassword;
        }
    }
}
