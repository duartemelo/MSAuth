using Hangfire;
using Microsoft.EntityFrameworkCore;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Application.Interfaces.Infrastructure.Communication;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using SharedEvents.User;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserConfirmationEmailAppService : IUserConfirmationAppService
    {
        private readonly IUserConfirmationService _userConfirmationService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;
        private readonly IEventProducer _eventProducer;

        public UserConfirmationEmailAppService(
            IUserConfirmationService userConfirmationService,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            NotificationContext notificationContext,
            IEventProducer eventProducer)
        {
            _userConfirmationService = userConfirmationService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
            _eventProducer = eventProducer;
        }

        public async Task<string?> Create(UserConfirmationCreateDTO confirmationCreate)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(confirmationCreate.Email);
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
            var userConfirmation = await _unitOfWork.UserConfirmationRepository.GetByTokenAsync(validation.Token);

            if (userConfirmation == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_CONFIRMATION_NOT_FOUND, string.Empty);
                return false;
            }

            if (!_userConfirmationService.Confirm(userConfirmation))
            {
                return false;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return false;
            }

            var user = userConfirmation.User;

            var userCreateEvent = new UserCreatedEvent
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };

            await _eventProducer.PublishAsync(userCreateEvent); // could implement here an outbox pattern for resilience

            return true;
        }
    }
}
