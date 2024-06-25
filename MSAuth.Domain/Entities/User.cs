using Microsoft.AspNetCore.Identity;

namespace MSAuth.Domain.Entities
{
    public class User : IdentityUser
    {
        public App App { get; set; }
        public DateTime DateOfRegister { get; set; }
        public DateTime? DateOfModification { get; set; }
        public DateTime? DateOfLastAccess { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire { get; set; }

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

        public void UpdateRefreshToken(string refreshToken, int expiresHours)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpire = DateTime.UtcNow.AddHours(expiresHours);
        }
    }
}
