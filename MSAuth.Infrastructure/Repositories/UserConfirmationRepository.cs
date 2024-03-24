using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Infrastructure.Data;

namespace MSAuth.Infrastructure.Repositories
{
    public class UserConfirmationRepository : BaseRepository<UserConfirmation>, IUserConfirmationRepository
    {
        public UserConfirmationRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
