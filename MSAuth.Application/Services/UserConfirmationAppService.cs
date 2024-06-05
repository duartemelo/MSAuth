using Hangfire;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;

namespace MSAuth.Application.Services
{
    public class UserConfirmationAppService : IUserConfirmationAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserConfirmationService _userConfirmationService;
        private readonly IEmailService _emailService;

        public UserConfirmationAppService(IUnitOfWork unitOfWork, IUserConfirmationService userConfirmationService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _userConfirmationService = userConfirmationService;
            _emailService = emailService;
        }

        public void SendUserConfirmation(User user, string appKey)
        {
            BackgroundJob.Enqueue(() => SendUserConfirmationJob(user.Id, user.Email!, appKey));
            Console.WriteLine("Job was sent to queue!");
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendUserConfirmationJob(string userId, string userEmail, string appKey)
        {
            var emailResult = await _emailService.Send(userEmail);
            if (emailResult == true)
            {
                await CreateUserConfirmationAsync(userId, appKey);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task CreateUserConfirmationAsync(string userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);

            if (user == null)
                return;

            _userConfirmationService.CreateUserConfirmation(user);
        }
    }
}
