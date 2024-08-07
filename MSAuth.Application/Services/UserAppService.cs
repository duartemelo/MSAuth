﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MSAuth.Application.Interfaces;
using MSAuth.Application.Interfaces.Infrastructure;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
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

        public UserAppService(IUnitOfWork unitOfWork, IUserService userService, NotificationContext notificationContext, IMapper mapper, IUserConfirmationService userConfirmationService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _notificationContext = notificationContext;
            _mapper = mapper;
            _userConfirmationService = userConfirmationService;
            _tokenService = tokenService;
        }

        public async Task<UserGetDTO?> GetUserByIdAsync(long userId, string appKey)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, appKey);
            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);   
            }
            return _mapper.Map<UserGetDTO>(user);
        }

        public async Task<UserCreateResponseDTO?> CreateUserAsync(UserCreateDTO user, string appKey)
        {
            var app = await _unitOfWork.AppRepository.GetByAppKeyAsync(appKey);
            if (app == null)
            {
                _notificationContext.AddNotification(NotificationKeys.APP_NOT_FOUND, string.Empty);
                return null;
            } 

            var createdUser = await _userService.CreateUserAsync(user, app);
            if (createdUser == null)
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
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

        public async Task<UserLoginResponseDTO?> Login(UserLoginDTO user, string appKey)
        {
            var existentUser = await _unitOfWork.UserRepository.GetByEmailAsync(user.Email, appKey);

            if (existentUser == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND, string.Empty);
                return null;
            }

            if (!existentUser.IsConfirmed)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_IS_NOT_CONFIRMED, string.Empty);
                return null;
            }

            if (!existentUser.ValidatePassword(user.Password))
            {
                _notificationContext.AddNotification(NotificationKeys.INVALID_USER_CREDENTIALS, string.Empty);
                return null;
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            var token = _tokenService.GenerateToken(existentUser);

            _userService.UpdateRefreshToken(existentUser, refreshToken);

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

        public async Task<UserLoginResponseDTO?> Refresh(string refreshToken, string appKey)
        {
            // TODO: Implement caching?

            var existentUser = await _unitOfWork.UserRepository.GetByRefreshTokenAsync(refreshToken, appKey);
            if (existentUser == null)
            {
                _notificationContext.AddNotification(NotificationKeys.INVALID_REFRESH_TOKEN, string.Empty);
                return null;
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var newToken = _tokenService.GenerateToken(existentUser);

            _userService.UpdateRefreshToken(existentUser, newRefreshToken);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR, string.Empty);
                return null;
            }

            return new()
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            };
        }
    }
}
