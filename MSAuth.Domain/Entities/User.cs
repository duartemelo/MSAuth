using MSAuth.Domain.Utils;
using static MSAuth.Domain.Utils.PasswordGeneration;

namespace MSAuth.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? ExternalId { get; set; }
        public App? App { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public DateTime? DateOfLastAccess { get; set; }

        // Construtor necessário para EF
        private User()
        {
        }

        // TODO: pass to domain
        public User(string externalId, App app, string email, string password)
        {
            ExternalId = externalId;

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("E-mail cannot be empty.", nameof(email));

            PasswordValidation.ValidatePassword(password);

            App = app;
            Email = email;

            Password generatedPassword = GeneratePassword(password);
            PasswordHash = generatedPassword.hash;
            PasswordSalt = generatedPassword.salt;
        }
    }
}
