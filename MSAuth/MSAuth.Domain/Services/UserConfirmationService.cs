using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Domain.Services
{
    public class UserConfirmationService : IUserConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;

        public UserConfirmationService(
            IUnitOfWork unitOfWork, 
            NotificationContext notificationContext)
        {
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        public async Task<UserConfirmation> CreateUserConfirmationAsync(User user)
        {
            var userConfirmation = new UserConfirmation(user);
            return await _unitOfWork.UserConfirmationRepository.AddAsync(userConfirmation);
        }

        public bool Confirm(UserConfirmation userConfirmation)
        {
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

            return true;
        }
    }
}
