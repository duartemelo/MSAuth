using MSAuth.Domain.ValueObjects;

namespace MSAuth.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; }
        public string FirstName { get; private set;  }
        public string LastName { get; private set;  }
        public Password Password { get; private set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorActivated { get; set; } = false;
        public int AccessFailedCount { get; set; } = 0;
        public DateTime? LockoutEnd { get; set; }
        public DateTime? DateOfLastAccess { get; set; }
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpire { get; private set; }
        public virtual ICollection<UserConfirmation> UserConfirmations { get; set; } = [];
        public bool IsConfirmed => UserConfirmations.Any(x => x.DateOfConfirm != null);
        public UserClaims Claims => new(this);

        // Construtor necessário para EF
        protected User() {}

        public User(string email,  string firstName, string lastName, string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            SetPassword(password);
        }

        public void UpdateRefreshToken(string refreshToken, int expiresHours)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpire = DateTime.UtcNow.AddHours(expiresHours);
        }

        public void SetPassword(string password)
        {
            Password = new Password(password);
        }

        public bool ValidatePassword(string password)
        {
            return Password.Validate(password);
        }

        public void UpdateLastAccessDate()
        {
            DateOfLastAccess = DateTime.UtcNow;
        }

        
    }
}
