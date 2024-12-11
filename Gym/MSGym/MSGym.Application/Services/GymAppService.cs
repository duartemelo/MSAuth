using AutoMapper;
using MSGym.Application.Interfaces;
using MSGym.Domain.DTOs;
using MSGym.Domain.Interfaces.Services;

namespace MSGym.Application.Services
{
    public class GymAppService : IGymAppService
    {
        private readonly IGymService _gymService;
        private readonly IMapper _mapper;
        public GymAppService(IGymService gymService, IMapper mapper)
        {
            _gymService = gymService;
            _mapper = mapper;
        }

        public async Task<GymCreateDTO?> CreateGymAsync(GymCreateDTO gymToCreate)
        {
            var createdGym = await _gymService.CreateGymAsync(gymToCreate);
            return _mapper.Map<GymCreateDTO>(createdGym);
        }
    }
}
