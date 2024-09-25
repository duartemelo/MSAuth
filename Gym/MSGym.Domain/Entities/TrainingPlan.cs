namespace MSGym.Domain.Entities
{
    public class TrainingPlan : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Role> ResponsibleRoles { get; set; }
        public string CreationUserEmail { get; set; }
        public User Athlete { get; set; }
        public ICollection<TrainingPlanExercise> Exercises { get; set; }

        private TrainingPlan() { }

        public TrainingPlan(string name, ICollection<Role> responsibleRoles, string creationUserEmail, User athlete, ICollection<TrainingPlanExercise> exercises, string? description = null)
        {
            Name = name;
            ResponsibleRoles = responsibleRoles;
            CreationUserEmail = creationUserEmail;
            Athlete = athlete;
            Exercises = exercises;
            Description = description;
        }
    }
}
