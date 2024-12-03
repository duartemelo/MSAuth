namespace MSGym.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public string CreationUserEmail { get; set; }
        public DateTime? ExpiryDate { get; set; }

        private UserRole() { }

        public UserRole(User user, Role role, string creationUserEmail, DateTime? expiryDate = null)
        {
            User = user;
            Role = role;
            CreationUserEmail = creationUserEmail;

            if (role.Duration.HasValue)
            {
                ExpiryDate = DateTime.UtcNow.Add(role.Duration.Value); // if role has a default duration, append the duration to datetime.now
            }

            if (expiryDate != null)
            {
                ExpiryDate = expiryDate; // if was specified an expiry date, set that expiry date to the entity
            }
        }
    }
}
