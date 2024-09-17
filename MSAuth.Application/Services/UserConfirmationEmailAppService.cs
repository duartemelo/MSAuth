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
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;

        public UserConfirmationEmailAppService(
            IUserConfirmationService userConfirmationService, 
            IEmailService emailService, 
            IUnitOfWork unitOfWork, 
            NotificationContext notificationContext)
        {
            _userConfirmationService = userConfirmationService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        public async Task<string?> Create(UserConfirmationCreateDTO confirmationCreate)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(confirmationCreate.UserId);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);
                return null;
            }

            if (user.IsConfirmed)
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

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            BackgroundJob.Enqueue(() => SendUserConfirmationJob(user.Email!, userConfirmation.Token));

            return userConfirmation.Token;
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendUserConfirmationJob(string userEmail, string userConfirmationToken)
        {
            await _emailService.Send(userEmail, userConfirmationToken);
        }

        public async Task<bool> Confirm(UserConfirmationValidateDTO validation)
        {
            await _userConfirmationService.Confirm(validation.Token);
            return await _unitOfWork.CommitAsync();
        }
    }
}
