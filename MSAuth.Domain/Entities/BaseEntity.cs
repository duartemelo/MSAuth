namespace MSAuth.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? DateOfModification {  get; set; }

        public BaseEntity() {}
    }
}
