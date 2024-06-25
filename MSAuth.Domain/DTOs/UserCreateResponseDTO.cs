namespace MSAuth.Domain.DTOs
{
    public class UserCreateResponseDTO
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfRegister { get; set; }
        public DateTime? DateOfModification { get; set; }
        public DateTime? DateOfLastAccess { get; set; }
        public string? ConfirmationToken { get; set; }
    }
}
