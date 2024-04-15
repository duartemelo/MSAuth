using MSAuth.Domain.DTOs;

namespace MSAuth.Application.Interfaces
{
    public interface IAppAppService
    {
        Task<AppCreateDTO> CreateAppAsync();
    }
}