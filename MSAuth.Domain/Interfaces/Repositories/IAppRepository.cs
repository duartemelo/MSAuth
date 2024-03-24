using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IAppRepository : IBaseRepository<App>
    {
        Task<App?> GetByAppKeyAsync(string appKey);
    }
}
