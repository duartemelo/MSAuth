namespace MSAuth.Application.Interfaces
{
    public interface IUserConfirmationAppService
    {
        Task CreateUserConfirmationAsync(int userId, string appKey);
        void SendUserConfirmation(int userId, string appKey);
        Task SendUserConfirmationJob(int userId, string userEmail, string appKey);
    }
}