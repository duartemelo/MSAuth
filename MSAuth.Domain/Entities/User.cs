using MSAuth.Domain.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSAuth.Domain.DomainServices.SharedUtilities;

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

        public User(string externalId, App app, string email, string password)
        {
            ExternalId = externalId;

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("E-mail cannot be empty.", nameof(email));

            PasswordValidationService.ValidatePassword(password);

            App = app;
            Email = email;

            Password generatedPassword = GeneratePassword(password);
            PasswordHash = generatedPassword.hash;
            PasswordSalt = generatedPassword.salt;

            DateOfRegister = DateTime.UtcNow;
        }

        public void UpdateUser()
        {
            DateOfModification = DateTime.UtcNow;
        }
    }
}
