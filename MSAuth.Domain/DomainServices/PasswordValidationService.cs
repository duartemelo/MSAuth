using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.DomainServices
{
    public class PasswordValidationService
    {
        public static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("The password cannot be empty.", nameof(password));

            if (password.Length < 8)
                throw new ArgumentException("The password must have at least 8 characters.", nameof(password));

            if (!ContainsUpperCaseLetter(password))
                throw new ArgumentException("The password must have at least 1 upcase letter.", nameof(password));

            if (!ContainsDigit(password))
                throw new ArgumentException("The password must have at least 1 number.", nameof(password));
        }

        private static bool ContainsUpperCaseLetter(string password)
        {
            return password.Any(char.IsUpper);
        }

        private static bool ContainsDigit(string password)
        {
            return password.Any(char.IsDigit);
        }
    }
}
