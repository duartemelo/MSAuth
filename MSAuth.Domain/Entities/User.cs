using MSAuth.Domain.Utils;
using static MSAuth.Domain.Utils.PasswordGeneration;

namespace MSAuth.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? ExternalId { get; set; }
        public App? App { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public DateTime DateOfRegister { get; set; }
        public DateTime? DateOfModification { get; set; }
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

            DateOfRegister = DateTime.UtcNow;
        }

        // TODO: create base entity with DateOfCreation and DateOfModification and all entities inherit that 
        // then, create an Update method that can update any base entity, updating their DateOfModification
        // and a Create method that can create any base entity, setting their DateOfCreation
        public void UpdateUser()
        {
            DateOfModification = DateTime.UtcNow;
        }
    }
}
