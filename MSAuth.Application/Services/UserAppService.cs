using AutoMapper;
using Microsoft.Extensions.Configuration;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Interfaces.Persistence.CachedRepositories;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;
        private readonly IUserConfirmationService _userConfirmationService;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenCachedRepository _refreshTokenCachedRepository;
        private readonly IConfiguration _configuration;

        public UserAppService(IUnitOfWork unitOfWork, IUserService userService, NotificationContext notificationContext, IMapper mapper, IUserConfirmationService userConfirmationService, ITokenService tokenService, IRefreshTokenCachedRepository refreshTokenCachedRepository, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _notificationContext = notificationContext;
            _mapper = mapper;
            _userConfirmationService = userConfirmationService;
            _tokenService = tokenService;
            _refreshTokenCachedRepository = refreshTokenCachedRepository;
            _configuration = configuration;
        }

        public async Task<UserGetDTO?> GetUserByIdAsync(long userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);   
            }
            return _mapper.Map<UserGetDTO>(user);
        }

        public async Task<UserCreateResponseDTO?> CreateUserAsync(UserCreateDTO user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            if (createdUser == null)
            {
                return null;
            }

            var userConfirmation = await _userConfirmationService.CreateUserConfirmationAsync(createdUser);
            
            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            var response = _mapper.Map<UserCreateResponseDTO>(createdUser);
            response.ConfirmationToken = userConfirmation.Token;

            return response;
        }

        public async Task<UserLoginResponseDTO?> Login(UserLoginDTO user)
        {
            var existentUser = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email);

            if (existentUser == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);
                return null;
            }

            if (!_userService.ValidateUserForLogin(user, existentUser))
            {
                return null;
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            var token = _tokenService.GenerateToken(existentUser);
            int expiresHoursRefreshToken = int.Parse(_configuration.GetSection("RefreshToken:ExpiresHours").Value!);

            await _refreshTokenCachedRepository.SetAsync(refreshToken, existentUser.Id, expiresHoursRefreshToken);

            existentUser.UpdateLastAccessDate();

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return new()
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<UserLoginResponseDTO?> Refresh(string refreshToken)
        {
            long? userId = await _refreshTokenCachedRepository.GetUserIdByRefreshTokenAsync(refreshToken);

            if (userId == null)
            {
                _notificationContext.AddNotification(NotificationKeys.INVALID_REFRESH_TOKEN, string.Empty);
                return null;
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync((long)userId); // TODO: instead of doing this, store claims on cache too!

            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.INVALID_REFRESH_TOKEN, string.Empty);
                return null;
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newToken = _tokenService.GenerateToken(user);
            int expiresHoursRefreshToken = int.Parse(_configuration.GetSection("RefreshToken:ExpiresHours").Value!);

            await _refreshTokenCachedRepository.RemoveAsync(refreshToken); // remove old refresh token
            await _refreshTokenCachedRepository.SetAsync(newRefreshToken, (long)userId, expiresHoursRefreshToken); // add new refresh token

            return new()
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
