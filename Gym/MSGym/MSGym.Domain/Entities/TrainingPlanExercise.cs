namespace MSGym.Domain.Entities
{
    public class TrainingPlanExercise : BaseEntity
    {
        public Exercise Exercise { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public string? Observations { get; set; }

        private TrainingPlanExercise() { }

        public TrainingPlanExercise(Exercise exercise, int sets, int repetitions, string? observations = null)
        {
            Exercise = exercise;
            Sets = sets;
            Repetitions = repetitions;
            Observations = observations;
        }
    }
}
