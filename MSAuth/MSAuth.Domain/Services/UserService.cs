using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IValidator<UserCreateDTO> _userCreateDTOValidator;
        private readonly EntityValidationService _entityValidationService;
        private readonly NotificationContext _notificationContext;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IValidator<UserCreateDTO> userCreateDTOValidator, EntityValidationService entityValidationService, IConfiguration configuration, NotificationContext notificationContext, IUnitOfWork unitOfWork)
        {
            _userCreateDTOValidator = userCreateDTOValidator;
            _entityValidationService = entityValidationService;
            _configuration = configuration;
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> CreateUserAsync(UserCreateDTO userToCreate)
        {
            var validationResult = _entityValidationService.Validate(_userCreateDTOValidator, userToCreate);
            if (!validationResult)
            {
                return null;
            }

            var userExists = await _unitOfWork.UserRepository.GetUserExists(userToCreate.Email);
            if (userExists)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_ALREADY_EXISTS, string.Empty);
                return null;
            }

            var user = new User(userToCreate.Email, userToCreate.FirstName, userToCreate.LastName, userToCreate.Password);

            return await _unitOfWork.UserRepository.AddAsync(user);
        }

        public void UpdateRefreshToken(User user, string refreshToken)
        {
            int expiresHoursRefreshToken = int.Parse(_configuration.GetSection("RefreshToken:ExpiresHours").Value!);
            user.UpdateRefreshToken(refreshToken, expiresHoursRefreshToken);
        }

        public bool ValidateUserForLogin(UserLoginDTO requestUser, User existentUser)
        {
            if (!existentUser.ValidatePassword(requestUser.Password))
            {
                _notificationContext.AddNotification(NotificationKeys.INVALID_USER_CREDENTIALS, string.Empty);
                return false;
            }

            if (!existentUser.IsConfirmed)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_IS_NOT_CONFIRMED, string.Empty);
                return false;
            }

            return true;
        }
    }
}
