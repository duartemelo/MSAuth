using MSAuth.Domain.DTOs;

namespace MSAuth.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserCreateResponseDTO?> CreateUserAsync(UserCreateDTO user, string appKey);
        Task<UserGetDTO?> GetUserByIdAsync(string userId, string appKey);
        Task<UserLoginResponseDTO?> Login(UserLoginDTO user, string appKey);
        Task<UserLoginResponseDTO?> Refresh(string refreshToken, string v);
    }
}