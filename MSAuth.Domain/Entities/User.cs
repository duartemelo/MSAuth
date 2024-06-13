using Microsoft.AspNetCore.Identity;

namespace MSAuth.Domain.Entities
{
    public class User : IdentityUser
    {
        public App App { get; set; }
        public DateTime DateOfRegister { get; set; }
        public DateTime? DateOfModification { get; set; }
        public DateTime? DateOfLastAccess { get; set; }

        // Construtor necessário para EF
        private User()
        {
        }
        public User(App app, string email)
        {
            App = app;
            UserName = email;
            Email = email;
        }
    }
}
