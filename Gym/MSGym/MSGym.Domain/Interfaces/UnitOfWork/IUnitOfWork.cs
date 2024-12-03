using MSGym.Domain.Entities;
using MSGym.Domain.Interfaces.Persistence.Repositories;

namespace MSGym.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        public IBaseRepository<User> UserRepository { get; }
    }
}
