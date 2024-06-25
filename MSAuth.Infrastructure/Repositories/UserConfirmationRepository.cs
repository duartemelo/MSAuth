using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Repositories
{
    public class UserConfirmationRepository : BaseRepository<UserConfirmation>, IUserConfirmationRepository
    {
        private readonly ApplicationDbContext _context;
        public UserConfirmationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserConfirmation?> GetByTokenAsync(string token, App app)
        {
            return await _context.UserConfirmations.FirstOrDefaultAsync(x => x.Token == token && x.User!.App == app);
        }
    }
}
