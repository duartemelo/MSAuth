using Hangfire;
using MSAuth.Application.Interfaces;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;

namespace MSAuth.Application.Services
{
    public class UserConfirmationAppService : IUserConfirmationAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserConfirmationService _userConfirmationService;

        public UserConfirmationAppService(IUnitOfWork unitOfWork, IUserConfirmationService userConfirmationService)
        {
            _unitOfWork = unitOfWork;
            _userConfirmationService = userConfirmationService;
        }

        public async void SendUserConfirmation(int userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);

            if (user == null || user.Email == null)
                return;

            BackgroundJob.Enqueue(() => SendUserConfirmationJob(userId, user.Email, appKey));
            Console.WriteLine("Job was sent to queue!");
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendUserConfirmationJob(int userId, string userEmail, string appKey)
        {
            var emailResult = await SendEmailMock(userEmail);
            if (emailResult == true)
            {
                await CreateUserConfirmationAsync(userId, appKey);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task CreateUserConfirmationAsync(int userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);

            if (user == null)
                return;

            _userConfirmationService.CreateUserConfirmation(user);
        }

        // TODO: separate email communication to infra
        private static async Task<bool> SendEmailMock(string userEmail)
        {
            await Task.Delay(40000);
            Console.WriteLine("Email was sent! " + userEmail);
            return true;
        }
    }
}
