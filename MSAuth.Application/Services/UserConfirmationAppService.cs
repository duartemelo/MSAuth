using Hangfire;
using Microsoft.EntityFrameworkCore;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserConfirmationAppService : IUserConfirmationAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserConfirmationService _userConfirmationService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly NotificationContext _notificationContext;

        public UserConfirmationAppService(IUnitOfWork unitOfWork, IUserConfirmationService userConfirmationService, IEmailService emailService, NotificationContext notificationContext, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userConfirmationService = userConfirmationService;
            _emailService = emailService;
            _notificationContext = notificationContext;
            _userService = userService;
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

            await _userConfirmationService.CreateUserConfirmationAsync(user);
        }

        public async Task<bool> Confirm(UserConfirmationValidateDTO validation, string appKey)
        {
            var app = await _unitOfWork.AppRepository.GetByAppKeyAsync(appKey);
            if (app == null)
            {
                _notificationContext.AddNotification(NotificationKeys.APP_NOT_FOUND, string.Empty);
                return false;
            }

            await _userConfirmationService.Confirm(validation.Token, app);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<string?> Create(UserConfirmationCreateDTO confirmationCreate, string appKey)
        {
            var app = await _unitOfWork.AppRepository.GetByAppKeyAsync(appKey);
            if (app == null)
            {
                _notificationContext.AddNotification(NotificationKeys.APP_NOT_FOUND, string.Empty);
                return null;
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(confirmationCreate.UserId, appKey);
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

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return userConfirmation.Token;
        }
    }
}
