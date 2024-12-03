namespace MSAuth.Domain.DTOs
{
    public class UserGetDTO
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? DateOfModification { get; set; }
        public DateTime? DateOfLastAccess { get; set; }
    }
}
