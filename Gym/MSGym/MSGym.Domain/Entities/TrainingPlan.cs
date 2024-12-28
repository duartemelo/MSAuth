namespace MSGym.Domain.Entities
{
    // TODO: 
    // training plan can be created without associating athlete, just for "template" purposes
    public class TrainingPlan : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Role> ResponsibleRoles { get; set; } // TODO: fix! this is not applied in database!
        public string CreationUserEmail { get; set; }
        public User Athlete { get; set; }
        public virtual ICollection<TrainingPlanExercise> Exercises { get; set; }

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
