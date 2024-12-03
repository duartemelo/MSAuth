using Microsoft.EntityFrameworkCore;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Persistence.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Persistence.Repositories
{
    public class UserConfirmationRepository : BaseRepository<UserConfirmation>, IUserConfirmationRepository
    {
        private readonly ApplicationDbContext _context;
        public UserConfirmationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserConfirmation?> GetByTokenAsync(string token)
        {
            return await _context.UserConfirmations.FirstOrDefaultAsync(x => x.Token == token);
        }
    }
}
