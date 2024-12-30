namespace MSGym.Domain.Entities
{
    /// <summary>
    /// The user entity was created because if we didn't do it, we would make a lot of requests to our authentication microservice.
    /// This would impact MSGym performance and could lead into cascading failure (if MSAuth is down you can't do anything on MSGym)
    /// On the other hand, MSAuth must be responsible to send async events to update this "copy" entity
    /// For this reason, a user was maintained on the Gym API side and it is synchronized asynchronously between the two MS.
    /// </summary>
    public class User : BaseEntity
    {
        public long ExternalId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual List<TrainingPlan> TrainingPlans { get; } = [];

        protected User() { }

        public User(long externalId, string email, string firstName, string lastName, string? phoneNumber)
        {
            ExternalId = externalId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}
