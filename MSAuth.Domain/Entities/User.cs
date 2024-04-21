using Microsoft.AspNetCore.Identity;

namespace MSAuth.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? ExternalId { get; set; }
        public App? App { get; set; }
        public DateTime? DateOfRegister { get; set; }
        public DateTime? DateOfModification { get; set; }
        public DateTime? DateOfLastAccess { get; set; }

        // Construtor necessário para EF
        private User()
        {
        }
        public User(string externalId, App app, string email)
        {
            ExternalId = externalId;
            App = app;
            UserName = email;
            Email = email;
        }
    }
}
