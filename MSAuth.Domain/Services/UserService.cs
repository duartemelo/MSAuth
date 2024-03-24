using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;

namespace MSAuth.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User CreateUser(UserCreateDTO userToCreate, App app)
        {
            var user = new User(userToCreate.ExternalId, app, userToCreate.Email, userToCreate.Password);
            return _unitOfWork.UserRepository.Add(user);
        }
    }
}
