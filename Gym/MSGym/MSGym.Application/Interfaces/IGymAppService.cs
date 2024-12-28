using MSGym.Domain.DTOs;

namespace MSGym.Application.Interfaces
{
    public interface IGymAppService
    {
        Task<GymCreateDTO?> CreateGymAsync(GymCreateDTO gymToCreate);
        Task<bool> DeleteGymAsync(long id);
    }
}
