using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string userId, string appKey);
        Task<User?> GetByEmailAsync(string email, string appKey);
        Task<Boolean> GetUserExistsSameApp(string email, string appKey);
        void Update(User user);
        Task<User?> GetByRefreshTokenAsync(string refreshToken, string appKey);
    }
}
