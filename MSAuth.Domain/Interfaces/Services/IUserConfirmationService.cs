using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Services
{
    public interface IUserConfirmationService
    {
        Task<UserConfirmation> CreateUserConfirmationAsync(User user);
        Task<bool> Confirm(string token);
    }
}