namespace MSGym.Domain.DTOs
{
    public class GymCreateDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Address { get; set; }
        public required string ZipCode { get; set; }
        public required string Email { get; set; }
    }
}
