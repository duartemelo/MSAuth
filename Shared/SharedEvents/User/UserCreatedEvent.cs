namespace SharedEvents.User
{
    public class UserCreatedEvent
    {
        public long UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber {  get; set; }
    }
}
