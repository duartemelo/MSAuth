using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByIdAsync(long userId, string appKey);
        Task<User?> GetByEmailAsync(string email, string appKey);
        Task<bool> GetUserExistsSameApp(string email, string appKey);
        Task<User?> GetByRefreshTokenAsync(string refreshToken, string appKey);
    }
}
