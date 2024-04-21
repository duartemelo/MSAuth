namespace MSAuth.Application.Interfaces
{
    public interface IUserConfirmationAppService
    {
        Task CreateUserConfirmationAsync(string userId, string appKey);
        void SendUserConfirmation(string userId, string appKey);
        Task SendUserConfirmationJob(string userId, string userEmail, string appKey);
    }
}