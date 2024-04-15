using AutoMapper;
using MSAuth.Application.Interfaces;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IUserConfirmationAppService _userConfirmationAppService;
        private readonly ModelErrorsContext _modelErrorsContext;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;

        public UserAppService(IUnitOfWork unitOfWork, IUserService userService, NotificationContext notificationContext, IMapper mapper, IUserConfirmationAppService userConfirmationAppService, ModelErrorsContext modelErrorsContext)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _notificationContext = notificationContext;
            _mapper = mapper;
            _userConfirmationAppService = userConfirmationAppService;
            _modelErrorsContext = modelErrorsContext;
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

            if (createdUser != null && !await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
            }

            if (createdUser != null)
                _userConfirmationAppService.SendUserConfirmation(createdUser.Id, appKey);

            return _mapper.Map<UserGetDTO>(createdUser);
        }

    }
}
