using static MSGym.Domain.Constants.Enums;

namespace MSGym.Domain.Entities
{
    public class GymContact : BaseEntity
    {
        public ContactType ContactType { get; set; }
        public string Contact {  get; set; }
        public Gym Gym { get; set; }
        public User CreationUser { get; set; }

        public GymContact(ContactType contactType, string contact, Gym gym, User creationUser) 
        {
            ContactType = contactType;
            Contact = contact;
            Gym = gym;
            CreationUser = creationUser;
        }
    }
}
