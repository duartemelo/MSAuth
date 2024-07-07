using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByIdAsync(long userId, string appKey)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == userId && u.App.AppKey == appKey);
        }

        public async Task<User?> GetByEmailAsync(string email, string appKey)
        {
            return await GetEntity()
                .Include(x => x.UserConfirmations)
                .FirstOrDefaultAsync(user => user.Email == email && user.App.AppKey == appKey);
        }

        public async Task<bool> GetUserExistsSameApp(string email, string appKey)
        {
            return await GetEntity().AnyAsync(user => user.App.AppKey == appKey && user.Email == email);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, string appKey)
        {
            return await GetEntity()
                .FirstOrDefaultAsync(user => user.RefreshToken == refreshToken && user.RefreshTokenExpire > DateTime.UtcNow && user.App.AppKey == appKey);
        }
    }
}
