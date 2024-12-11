using AutoMapper;
using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;

namespace MSGym.Application.Mappings
{
    public class GymMappingProfile : Profile
    {
        public GymMappingProfile()
        {
            CreateMap<Gym, GymCreateDTO>();
        }
    }
}
