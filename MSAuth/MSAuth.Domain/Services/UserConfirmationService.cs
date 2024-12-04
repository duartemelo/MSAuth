using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Communication;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using SharedEvents.User;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Domain.Services
{
    public class UserConfirmationService : IUserConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;
        private readonly IEventProducer _eventProducer;

        public UserConfirmationService(
            IUnitOfWork unitOfWork, 
            NotificationContext notificationContext, 
            IEventProducer eventProducer)
        {
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
            _eventProducer = eventProducer;
        }

        public async Task<UserConfirmation> CreateUserConfirmationAsync(User user)
        {
            var userConfirmation = new UserConfirmation(user);
            return await _unitOfWork.UserConfirmationRepository.AddAsync(userConfirmation);
        }

        public async Task<bool> Confirm(string token)
        {
            var userConfirmation = await _unitOfWork.UserConfirmationRepository.GetByTokenAsync(token);

            if (userConfirmation == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_CONFIRMATION_NOT_FOUND, string.Empty);
                return false;
            }

            if (userConfirmation.IsExpired)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_CONFIRMATION_EXPIRED, string.Empty);
                return false;
            }
            
            if (userConfirmation.IsConfirmed)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_IS_ALREADY_CONFIRMED, string.Empty);
                return false;
            }

            userConfirmation.Confirm();

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

            await _eventProducer.PublishAsync(userCreateEvent); // TODO: outbox here for resilience

            return true;
        }
    }
}
