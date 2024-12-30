namespace MSGym.Domain.Entities
{
    public class ExerciseCategory : BaseEntity
    {
        public string Name { get; set; }
        public virtual Gym? Gym { get; set; } // exercise category can be or not generic
        public string? Description { get; set; }
        public string CreationUserEmail { get; set; }
        public virtual List<Exercise> Exercises { get; } = [];

        protected ExerciseCategory() { }

        public ExerciseCategory(string name, string creationUserEmail, string? description = null)
        {
            Name = name;
            CreationUserEmail = creationUserEmail;
            Description = description;
        }
    }
}
