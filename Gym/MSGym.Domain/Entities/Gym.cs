namespace MSGym.Domain.Entities
{
    public class Gym : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public User CreationUser { get; set; }

        private Gym() // For EF
        { }

        public Gym(string name, string address, string zipCode, string email, User creationUser)
        {
            Name = name; 
            Address = address; 
            ZipCode = zipCode; 
            Email = email; // TODO: add validation on domain service
            CreationUser = creationUser;
        }
    }
}
