using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Services
{
    public interface IAppService
    {
        Task<App> CreateApp();
    }
}
