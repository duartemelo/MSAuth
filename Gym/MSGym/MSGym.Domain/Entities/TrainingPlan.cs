namespace MSGym.Domain.Entities
{
    public class TrainingPlan : BaseEntity
    {
        public string Name { get; set; }
        public virtual Gym? Gym { get; set; } // training plan can be or not generic
        public string? Description { get; set; }
        public virtual List<Role> ResponsibleRoles { get; } = [];
        public string CreationUserEmail { get; set; }
        public virtual List<User> Athletes { get; } = [];
        public virtual ICollection<TrainingPlanExercise> Exercises { get; set; }

        protected TrainingPlan() { }

        public TrainingPlan(string name, List<Role> responsibleRoles, string creationUserEmail, List<User> athletes, string? description = null)
        {
            Name = name;
            ResponsibleRoles = responsibleRoles;
            CreationUserEmail = creationUserEmail;
            Athletes = athletes;
            Description = description;
        }
    }
}
