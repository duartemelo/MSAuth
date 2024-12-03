using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace MSAuth.Domain.ValueObjects
{
    public class Password
    {
        public byte[] Hash { get; private set; }
        public byte[] Salt { get; private set; }

        private Password() { }

        public Password(string plainTextPassword)
        {
            using var hmac = new HMACSHA256();
            Salt = hmac.Key;
            Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword));
        }

        public bool Validate(string plainTextPassword)
        {
            using var hmac = new HMACSHA256(Salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword));
            return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, Hash);
        }
    }
}
