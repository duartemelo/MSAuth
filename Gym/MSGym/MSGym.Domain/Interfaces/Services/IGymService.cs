using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;

namespace MSGym.Domain.Interfaces.Services
{
    public interface IGymService
    {
        Task<Gym?> CreateGymAsync(GymCreateDTO gymToCreate, string userEmail);
        Task<bool> DeleteGymAsync(long id, string requestUserEmail);
    }
}
