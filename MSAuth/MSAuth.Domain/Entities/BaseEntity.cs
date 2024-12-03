namespace MSAuth.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public DateTime? DateOfModification {  get; set; }

        public BaseEntity() {
            DateOfCreation = DateTime.Now;
        }
    }
}
