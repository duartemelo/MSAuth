namespace MSAuth.Domain.DTOs
{
    public class UserCreateDTO
    {
        public required string ExternalId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
