using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;

namespace MSGym.Domain.Interfaces.Services
{
    public interface IGymService
    {
        Task<Gym?> CreateGymAsync(GymCreateDTO gymToCreate);
    }
}
