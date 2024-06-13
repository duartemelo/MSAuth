using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MSAuth.Application.Interfaces;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
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
        private readonly UserManager<User> _userManager;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;

        public UserAppService(IUnitOfWork unitOfWork, IUserService userService, NotificationContext notificationContext, IMapper mapper, IUserConfirmationAppService userConfirmationAppService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _notificationContext = notificationContext;
            _mapper = mapper;
            _userConfirmationAppService = userConfirmationAppService;
            _userManager = userManager;
        }

        public async Task<UserGetDTO?> GetUserByIdAsync(string userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);   
            }
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

            var userExists = await _unitOfWork.UserRepository.GetUserExistsSameApp(user.Email, appKey);
            if (userExists)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_ALREADY_EXISTS, string.Empty);
                return null;
            }

            var createdUser = await _userService.CreateUserAsync(user, app);

            if (createdUser == null)
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
            else
                _userConfirmationAppService.SendUserConfirmation(createdUser, appKey);

            return _mapper.Map<UserGetDTO>(createdUser);
        }

        public async Task<string?> Login(UserLoginDTO user, string appKey)
        {
            var app = await _unitOfWork.AppRepository.GetByAppKeyAsync(appKey);
            if (app == null)
            {
                _notificationContext.AddNotification(NotificationKeys.APP_NOT_FOUND, string.Empty);
                return null;
            }

            var existentUser = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email, appKey);

            if (existentUser == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);
                return null;
            }

            if (await _userManager.CheckPasswordAsync(existentUser, user.Password))
            {
                var token = _userService.GenerateTokenString(existentUser);
                return token;
            }

            _notificationContext.AddNotification(NotificationKeys.INVALID_USER_CREDENTIALS, string.Empty);
            return null;
        }
    }
}
