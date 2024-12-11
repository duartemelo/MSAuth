using MSGym.Domain.Entities;
using MSGym.Domain.Interfaces.Persistence.Repositories;
using MSGym.Domain.Interfaces.UnitOfWork;
using MSGym.Infrastructure.Data;
using MSGym.Infrastructure.Persistence.Repositories;

namespace MSGym.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IBaseRepository<User> _userRepository = null!;
        private IBaseRepository<Gym> _gymRepository = null!;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public IBaseRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new BaseRepository<User>(_context);
                }
                return _userRepository;
            }
        }

        public IBaseRepository<Gym> GymRepository
        {
            get
            {
                if (_gymRepository == null)
                {
                    _gymRepository = new BaseRepository<Gym>(_context);
                }
                return _gymRepository;
            }
        }
    }
}
