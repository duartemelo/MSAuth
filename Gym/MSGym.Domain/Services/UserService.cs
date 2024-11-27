using Microsoft.EntityFrameworkCore;
using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;
using MSGym.Domain.Interfaces.Services;
using MSGym.Domain.Interfaces.UnitOfWork;

namespace MSGym.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> CreateUserAsync(UserCreateDTO userToCreate)
        {
            var userExists = await _unitOfWork.UserRepository.GetEntity().AnyAsync(x => x.Email == userToCreate.Email);

            if (userExists)
            {
                // TODO: notif context
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
                // TODO: notif context
                return null;
            }

            return createdUser;
        }
    }
}
