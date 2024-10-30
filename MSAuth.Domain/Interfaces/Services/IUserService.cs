using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> CreateUserAsync(UserCreateDTO userToCreate);
        void UpdateRefreshToken(User user, string refreshToken);
        bool ValidateUserForLogin(UserLoginDTO requestUser, User existentUser);
    }
}