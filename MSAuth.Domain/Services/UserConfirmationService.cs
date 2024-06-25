using Microsoft.EntityFrameworkCore.Query.Internal;
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

        public UserConfirmationService(IUnitOfWork unitOfWork, NotificationContext notificationContext)
        {
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        public async Task<UserConfirmation> CreateUserConfirmationAsync(User user)
        {
            var userConfirmation = new UserConfirmation(user);
            return await _unitOfWork.UserConfirmationRepository.AddAsync(userConfirmation);
        }

        public async Task<bool> Confirm(string token, App app)
        {
            var userConfirmation = await _unitOfWork.UserConfirmationRepository.GetByTokenAsync(token, app);

            if (userConfirmation == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_CONFIRMATION_NOT_FOUND, string.Empty);
                return false;
            }

            if (userConfirmation.DateOfExpire <=  DateTime.UtcNow)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_CONFIRMATION_EXPIRED, string.Empty);
                return false;
            } else
            {
                userConfirmation.DateOfConfirm = DateTime.Now;
                _unitOfWork.UserConfirmationRepository.Update(userConfirmation);
                return true;
            }
        }
    }
}
