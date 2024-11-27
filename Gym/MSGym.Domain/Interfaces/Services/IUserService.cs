using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;

namespace MSGym.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> CreateUserAsync(UserCreateDTO userToCreate);
    }
}
