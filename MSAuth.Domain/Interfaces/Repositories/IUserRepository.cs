using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string userId, string appKey);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> GetUserExistsSameAppByEmail(string email, string appKey);
    }
}
