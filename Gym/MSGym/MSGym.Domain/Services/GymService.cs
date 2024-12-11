using Microsoft.EntityFrameworkCore;
using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;
using MSGym.Domain.Interfaces.Services;
using MSGym.Domain.Interfaces.UnitOfWork;

namespace MSGym.Domain.Services
{
    public class GymService : IGymService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GymService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Gym?> CreateGymAsync(GymCreateDTO gymToCreate)
        {
            // TODO: validator
            var gymExists = await _unitOfWork.GymRepository
                .GetEntity()
                .AnyAsync(x => x.Name == gymToCreate.Name || x.Email == gymToCreate.Email);

            if (gymExists)
            {
                // TODO: notif context
                return null;
            }

            var userEmail = "postman2@temp.pt"; // TODO: gather user (how to get it from JWT?)

            var user = await _unitOfWork.UserRepository.GetEntity().FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user == null)
            {
                // TODO: notif context
                return null;
            }

            var gym = new Gym(gymToCreate.Name, gymToCreate.Address, gymToCreate.ZipCode, gymToCreate.Email, user);

            var createdGym = await _unitOfWork.GymRepository.AddAsync(gym);

            if (!await _unitOfWork.CommitAsync())
            {
                // TODO: notif context
                return null;
            }

            return createdGym;
        }
    }
}
