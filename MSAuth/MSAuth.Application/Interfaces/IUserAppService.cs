using MSAuth.Domain.DTOs;

namespace MSAuth.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserCreateResponseDTO?> CreateUserAsync(UserCreateDTO user);
        Task<UserGetDTO?> GetUserByIdAsync(long userId);
        Task<UserLoginResponseDTO?> Login(UserLoginDTO user);
        Task<UserLoginResponseDTO?> Refresh(string refreshToken);
    }
}