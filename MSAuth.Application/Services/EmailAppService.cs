using Hangfire;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class EmailAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificationContext;

        public EmailAppService(IUnitOfWork unitOfWork, NotificationContext notificationContext)
        {
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendUserConfirmationJob(int userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);

            if (user != null && user.Email != null)
            {
                var emailResult = await SendEmailMock(user.Email);
                if (emailResult == true)
                {
                    var userConfirmationToCreate = new UserConfirmation(user);
                    _unitOfWork.UserConfirmationRepository.Add(userConfirmationToCreate);  
                }
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
            }
        }

        private static async Task<bool> SendEmailMock(string userEmail)
        {
            await Task.Delay(40000);
            Console.WriteLine("Email was sent! " + userEmail);
            return true;
        }
    }
}
