using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string userId, string appKey);
        Task<User?> GetByEmailAsync(string email);
        Task<Boolean> GetUserExistsSameApp(string email, string externalId, string appKey);
        Task<User?> GetByExternalIdAsync(string externalId, string appKey);
    }
}
