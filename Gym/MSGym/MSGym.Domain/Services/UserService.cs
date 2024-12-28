using Microsoft.EntityFrameworkCore;
using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;
using MSGym.Domain.Interfaces.Services;
using MSGym.Domain.Interfaces.UnitOfWork;
using MSGym.Domain.Notifications;
using static MSGym.Domain.Constants.Constants;

namespace MSGym.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notificatonContext;

        public UserService(IUnitOfWork unitOfWork, NotificationContext notificatonContext)
        {
            _unitOfWork = unitOfWork;
            _notificatonContext = notificatonContext;
        }

        public async Task<User?> CreateUserAsync(UserCreateDTO userToCreate)
        {
            var userExists = await _unitOfWork.UserRepository.GetEntity().AnyAsync(x => x.Email == userToCreate.Email);

            if (userExists)
            {
                _notificatonContext.AddNotification(NotificationKeys.USER_ALREADY_EXISTS);
                return null;
            }

            var user = new User(
                userToCreate.ExternalId, 
                userToCreate.Email, 
                userToCreate.FirstName, 
                userToCreate.LastName, 
                userToCreate.PhoneNumber);

            var createdUser = await _unitOfWork.UserRepository.AddAsync(user);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificatonContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR);
                return null;
            }

            return createdUser;
        }
    }
}
