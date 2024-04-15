using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Services
{
    public interface IUserConfirmationService
    {
        UserConfirmation? CreateUserConfirmation(User user);
    }
}