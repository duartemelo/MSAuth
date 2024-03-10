﻿using AutoMapper;
using Hangfire;
using MSAuth.Application.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.IRepositories;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppRepository _appRepository;
        private readonly EmailService _emailService;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IAppRepository appRepository, EmailService emailService, NotificationContext notificationContext, IMapper mapper)
        {
            _userRepository = userRepository;
            _appRepository = appRepository;
            _emailService = emailService;
            _notificationContext = notificationContext;
            _mapper = mapper;
        }

        public async Task<UserGetDTO?> GetUserByIdAsync(int userId, string appKey)
        {
            var user = await _userRepository.GetByIdAsync(userId, appKey);
            return _mapper.Map<UserGetDTO>(user);
        }

        public async Task<UserGetDTO?> CreateUserAsync(UserCreateDTO user, string appKey)
        {
            var app = await _appRepository.GetByAppKeyAsync(appKey);
            if (app == null)
            {
                _notificationContext.AddNotification(NotificationKeys.APP_NOT_FOUND, string.Empty);
                return null; 
            }

            var userExists = await _userRepository.GetUserExistsSameAppByEmail(user.Email, appKey);
            if (userExists)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_ALREADY_EXISTS, string.Empty);
                return null;
            }

            // TODO: change to domain
            User? userToCreate;
            try
            {
                userToCreate = new User(user.ExternalId, app, user.Email, user.Password);
            } catch (Exception ex)
            {
                _notificationContext.AddNotification(NotificationKeys.ENTITY_VALIDATION_ERROR, ex.Message);
                return null;
            }
            
            await _userRepository.AddAsync(userToCreate);

            // simulating sending an email
            BackgroundJob.Enqueue(() => _emailService.SendUserConfirmationJob(userToCreate.Id, appKey));
            Console.WriteLine("Job was sent to queue!");

            return _mapper.Map<UserGetDTO>(userToCreate);
        }
    }
}
