using Hangfire;
using Microsoft.EntityFrameworkCore;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserConfirmationEmailAppService : IUserConfirmationAppService
    {
        private readonly IUserConfirmationService _userConfirmationService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserConfirmationAppService _userConfirmationAppService;
        private readonly NotificationContext _notificationContext;

        public UserConfirmationEmailAppService(IUserConfirmationService userConfirmationService, IEmailService emailService, IUnitOfWork unitOfWork, NotificationContext notificationContext, IUserService userService, UserConfirmationAppService userConfirmationAppService)
        {
            _userConfirmationService = userConfirmationService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
            _userService = userService;
            _userConfirmationAppService = userConfirmationAppService;
        }

        public async Task<string?> Create(UserConfirmationCreateDTO confirmationCreate)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(confirmationCreate.UserId);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);
                return null;
            }

            var userIsConfimed = await _userService.ValidateUserIsConfirmed(user);
            if (userIsConfimed)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_IS_ALREADY_CONFIRMED, string.Empty);
                return null;
            }

            var alreadyExistsUserConfirmation = await _unitOfWork.UserConfirmationRepository.GetEntity().Where(x => x.User == user && x.DateOfExpire > DateTime.UtcNow).AnyAsync();

            if (alreadyExistsUserConfirmation)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_CONFIRMATION_ALREADY_EXISTS, string.Empty);
                return null;
            }

            var userConfirmation = await _userConfirmationService.CreateUserConfirmationAsync(user);

            BackgroundJob.Enqueue(() => SendUserConfirmationJob(user.Email!, userConfirmation.Token));

            Console.WriteLine("Job was sent to queue!");

            return userConfirmation.Token;
        }

        [AutomaticRetry(Attempts = 5)]
        private async Task SendUserConfirmationJob(string userEmail, string userConfirmationToken)
        {
            await _emailService.Send(userEmail, userConfirmationToken);
        }

        public async Task<bool> Confirm(UserConfirmationValidateDTO validation)
        {
            return await _userConfirmationAppService.Confirm(validation);
        }
    }
}
