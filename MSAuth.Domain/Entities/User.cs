using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace MSAuth.Domain.Entities
{
    public class User : BaseEntity
    {
        // TODO: add first name, last name, image path
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorActivated { get; set; } = false;
        public int AccessFailedCount { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; }
        public DateTime? DateOfLastAccess { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire { get; set; }
        public virtual ICollection<UserConfirmation> UserConfirmations { get; set; } = [];
        public bool IsConfirmed => UserConfirmations.Any(x => x.DateOfConfirm != null);
        public UserClaims Claims => new(this);

        // Construtor necessário para EF
        protected User() {}

        public User(string email, string password)
        {
            Email = email;
            SetPassword(password);
        }

        public void UpdateRefreshToken(string refreshToken, int expiresHours)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpire = DateTime.UtcNow.AddHours(expiresHours);
        }

        public void SetPassword(string password)
        {
            using var hmac = new HMACSHA256();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool ValidatePassword(string password)
        {
            using var hmac = new HMACSHA256(PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, PasswordHash);
        }

        public void UpdateLastAccessDate()
        {
            DateOfLastAccess = DateTime.UtcNow;
        }

        
    }
}
