namespace MSAuth.Domain.Entities
{
    public class UserClaims
    {
        public long Id { get; set; }
        public string Email { get; set; } = string.Empty;

        private UserClaims()
        {

        }

        public UserClaims(User user)
        {
            Id = user.Id;
            Email = user.Email;
        }
    }
}
