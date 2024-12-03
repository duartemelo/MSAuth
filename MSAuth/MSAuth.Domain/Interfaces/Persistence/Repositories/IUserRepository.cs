using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Persistence.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByIdAsync(long userId);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> GetUserExists(string email);
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
    }
}
