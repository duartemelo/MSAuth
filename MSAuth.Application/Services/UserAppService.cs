using AutoMapper;
using Hangfire;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.Notifications;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Application.Services
{
    public class UserAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailAppService _emailService;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;

        public UserAppService(IUnitOfWork unitOfWork, EmailAppService emailService, NotificationContext notificationContext, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
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
                _notificationContext.AddNotification(NotificationKeys.USER_ALREADY_EXISTS, string.Empty);

            if (!_notificationContext.HasNotifications && app != null) {
                // TODO: change to domain
                User? userToCreate;
                try
                {
                    userToCreate = new User(user.ExternalId, app, user.Email, user.Password);
                }
                catch (Exception ex)
                {
                    _notificationContext.AddNotification(NotificationKeys.ENTITY_VALIDATION_ERROR, ex.Message);
                    return null;
                }

                _unitOfWork.UserRepository.Add(userToCreate);
                if (!await _unitOfWork.CommitAsync())
                {
                    // TODO: add notification post failed
                } else
                {
                    // simulating sending an email
                    // TODO: put in background job only the communication with email service
                    BackgroundJob.Enqueue(() => _emailService.SendUserConfirmationJob(userToCreate.Id, appKey));
                    Console.WriteLine("Job was sent to queue!");
                }
                return _mapper.Map<UserGetDTO>(userToCreate);
            }

            return null;
        }
    }
}
