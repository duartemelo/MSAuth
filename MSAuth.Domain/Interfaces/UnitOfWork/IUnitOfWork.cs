using MSAuth.Domain.Interfaces.Repositories;

namespace MSAuth.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        public IAppRepository AppRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserConfirmationRepository UserConfirmationRepository { get; }
    }
}
