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

        public async Task<User?> GetByIdAsync(long userId)
        {
            return await GetEntity().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await GetEntity()
                .Include(x => x.UserConfirmations)
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<bool> GetUserExists(string email)
        {
            return await GetEntity().AnyAsync(user => user.Email == email);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await GetEntity()
                .FirstOrDefaultAsync(user => user.RefreshToken == refreshToken && user.RefreshTokenExpire > DateTime.UtcNow);
        }
    }
}
