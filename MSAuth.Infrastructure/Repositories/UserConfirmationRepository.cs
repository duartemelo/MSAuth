using MSAuth.Domain.Entities;
using MSAuth.Domain.IRepositories;
using MSAuth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
