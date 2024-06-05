using MSAuth.Domain.Entities;

namespace MSAuth.Application.Interfaces
{
    public interface IUserConfirmationAppService
    {
        Task CreateUserConfirmationAsync(string userId, string appKey);
        void SendUserConfirmation(User user, string appKey);
        Task SendUserConfirmationJob(string userId, string userEmail, string appKey);
    }
}