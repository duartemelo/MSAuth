using static MSGym.Domain.Constants.Enums;

namespace MSGym.Domain.Entities
{
    public class GymContact : BaseEntity
    {
        public ContactType ContactType { get; set; }
        public string Contact {  get; set; }
        public virtual Gym Gym { get; set; }
        public string CreationUserEmail { get; set; }

        protected GymContact() { }

        public GymContact(ContactType contactType, string contact, Gym gym, string creationUserEmail) 
        {
            ContactType = contactType;
            Contact = contact;
            Gym = gym;
            CreationUserEmail = creationUserEmail;
        }
    }
}
