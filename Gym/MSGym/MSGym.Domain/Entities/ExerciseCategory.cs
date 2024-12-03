namespace MSGym.Domain.Entities
{
    public class ExerciseCategory : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CreationUserEmail { get; set; }

        private ExerciseCategory() { }

        public ExerciseCategory(string name, string creationUserEmail, string? description = null)
        {
            Name = name;
            CreationUserEmail = creationUserEmail;
            Description = description;
        }
    }
}
