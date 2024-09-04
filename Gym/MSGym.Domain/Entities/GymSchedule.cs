namespace MSGym.Domain.Entities
{
    public class GymSchedule : BaseEntity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Gym Gym { get; set; }
        public User CreationUser { get; set; }

        public GymSchedule(DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, Gym gym, User creationUser) {
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            Gym = gym;
            CreationUser = creationUser;
        }
    }
}
