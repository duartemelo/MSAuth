namespace MSGym.Domain.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<ExerciseCategory> Categories { get; set; }
        public string CreationUserEmail { get; set; }

        private Exercise() { }

        public Exercise(string name, ICollection<ExerciseCategory> categories, string creationUserEmail, string? description = null)
        {
            Name = name;
            Description = description;
            Categories = categories;
            CreationUserEmail = creationUserEmail;
        }
    }
}
