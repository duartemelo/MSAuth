namespace MSGym.Domain.Entities
{
    public class GymSchedule : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Gym Gym { get; set; }
        public string CreationUserEmail { get; set; }

        private GymSchedule() { }

        public GymSchedule(DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, Gym gym, string creationUserEmail) {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            Gym = gym;
            CreationUserEmail = creationUserEmail;
        }
    }
}
