namespace MSAuth.Domain.Entities
{
    public class UserConfirmation : BaseEntity
    {
        public virtual User? User { get; set; }
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public DateTime? DateOfConfirm { get; set; }
        public DateTime DateOfExpire { get; set; } = DateTime.Now.AddHours(2);
        public bool IsExpired => DateOfExpire <= DateTime.UtcNow;
        public bool IsConfirmed => DateOfConfirm != null;

        protected UserConfirmation() { }

        public UserConfirmation(User user)
        {
            User = user;
        }

        public void Confirm()
        {
            DateOfConfirm = DateTime.UtcNow;
        }
    }
}
