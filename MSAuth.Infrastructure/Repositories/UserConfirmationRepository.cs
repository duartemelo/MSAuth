using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Repositories
{
    public class UserConfirmationRepository : IUserConfirmationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserConfirmationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserConfirmation> AddAsync(UserConfirmation userConfirmation)
        {
            _context.UserConfirmations.Add(userConfirmation);
            await _context.SaveChangesAsync();
            return userConfirmation;
        }
    }
}
