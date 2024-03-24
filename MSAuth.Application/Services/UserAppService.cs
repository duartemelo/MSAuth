using AutoMapper;
using Hangfire;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailAppService _emailService;
        private readonly IUserService _userService;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;

        public UserAppService(IUnitOfWork unitOfWork, EmailAppService emailService, IUserService userService ,NotificationContext notificationContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _userService = userService;
            _notificationContext = notificationContext;
            _mapper = mapper;
        }

        public async Task<UserGetDTO?> GetUserByIdAsync(int userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);
            return _mapper.Map<UserGetDTO>(user);
        }

        public async Task<UserGetDTO?> CreateUserAsync(UserCreateDTO user, string appKey)
        {
            var app = await _unitOfWork.AppRepository.GetByAppKeyAsync(appKey);
            if (app == null)
            {
                _notificationContext.AddNotification(NotificationKeys.APP_NOT_FOUND, string.Empty);
                return null;
            }

            var userExists = await _unitOfWork.UserRepository.GetUserExistsSameAppByEmail(user.Email, appKey);
            if (userExists)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_ALREADY_EXISTS, string.Empty);
                return null;
            }

            var createdUser = _userService.CreateUser(user, app);

            if (createdUser == null)
            {
                return null;
            }

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            SendUserConfirmation(createdUser.Id, appKey);

            return _mapper.Map<UserGetDTO>(createdUser);
        }


        // TODO: put only communication with external service in the job, separate this to UserConfirmationAppService
        private void SendUserConfirmation(int userId, string appKey)
        {
            BackgroundJob.Enqueue(() => _emailService.SendUserConfirmationJob(userId, appKey));
            Console.WriteLine("Job was sent to queue!");
        }
    }
}
