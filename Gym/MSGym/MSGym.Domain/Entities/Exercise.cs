namespace MSGym.Domain.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }
        public virtual Gym? Gym { get; set; } // exercise can be or not generic
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public virtual List<ExerciseCategory> Categories { get; } = [];
        public string CreationUserEmail { get; set; }

        protected Exercise() { }

        public Exercise(string name, List<ExerciseCategory> categories, string creationUserEmail, string? description = null)
        {
            Name = name;
            Description = description;
            Categories = categories;
            CreationUserEmail = creationUserEmail;
        }
    }
}
