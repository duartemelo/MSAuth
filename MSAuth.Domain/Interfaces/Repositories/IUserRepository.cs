using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByIdAsync(int userId, string appKey);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> GetUserExistsSameAppByEmail(string email, string appKey);
    }
}
