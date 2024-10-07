using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Persistence.Repositories
{
    public interface IUserConfirmationRepository : IBaseRepository<UserConfirmation>
    {
        Task<UserConfirmation?> GetByTokenAsync(string token);
    }
}
