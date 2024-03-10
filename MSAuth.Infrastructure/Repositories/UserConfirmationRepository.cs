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

        public UserConfirmation Add(UserConfirmation userConfirmation)
        {
            _context.UserConfirmations.Add(userConfirmation);
            return userConfirmation;
        }
    }
}
