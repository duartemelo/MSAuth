namespace MSGym.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Gym Gym { get; set; }
        public string Name { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool CanEditGym { get; set; } = false;
        public bool CanCreateRoles { get; set; } = false;
        public bool CanAssignRoles { get; set; } = false;

        public Role(Gym gym, string name, TimeSpan? duration = null)
        {
            Gym = gym;
            Name = name;
            Duration = duration;
        }
    }
}
