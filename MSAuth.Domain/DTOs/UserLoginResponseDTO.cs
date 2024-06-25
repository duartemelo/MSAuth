namespace MSAuth.Domain.DTOs
{
    public class UserLoginResponseDTO
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
}
