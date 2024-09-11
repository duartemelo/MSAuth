using MSAuth.Domain.Interfaces.Persistence.Repositories;

namespace MSAuth.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        public IUserRepository UserRepository { get; }
        public IUserConfirmationRepository UserConfirmationRepository { get; }
    }
}
