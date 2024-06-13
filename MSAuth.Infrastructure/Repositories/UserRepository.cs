using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string userId, string appKey)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(u => u.Id == userId && u.App.AppKey == appKey);
        }

        public async Task<User?> GetByEmailAsync(string email, string appKey)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(user => user.Email == email && user.App.AppKey == appKey);
        }

        public async Task<Boolean> GetUserExistsSameApp(string email, string appKey)
        {
            return await _context.AppUsers.AnyAsync(user => user.App.AppKey == appKey && user.Email == email);
        }
    }
}
