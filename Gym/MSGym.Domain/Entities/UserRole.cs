namespace MSGym.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public User CreationUser { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public UserRole(User user, Role role, User creationUser, DateTime? expiryDate = null)
        {
            User = user;
            Role = role;
            CreationUser = creationUser;

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
