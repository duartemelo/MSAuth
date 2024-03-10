using MSAuth.Domain.Interfaces.Repositories;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Infrastructure.Data;
using MSAuth.Infrastructure.Repositories;

namespace MSAuth.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IAppRepository _appRepository = null!;
        private IUserRepository _userRepository = null!;
        private IUserConfirmationRepository _userConfirmationRepository = null!;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public IAppRepository AppRepository
        {
            get
            {
                if (_appRepository == null)
                {
                    _appRepository = new AppRepository(_context);
                }
                return _appRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IUserConfirmationRepository UserConfirmationRepository
        {
            get
            {
                if (_userConfirmationRepository == null)
                {
                    _userConfirmationRepository = new UserConfirmationRepository(_context);
                }
                return _userConfirmationRepository;
            }
        }


    }
}
