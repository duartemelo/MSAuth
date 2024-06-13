using MSAuth.Domain.DTOs;

namespace MSAuth.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserGetDTO?> CreateUserAsync(UserCreateDTO user, string appKey);
        Task<UserGetDTO?> GetUserByIdAsync(string userId, string appKey);
        Task<string?> Login(UserLoginDTO user, string appKey);
    }
}