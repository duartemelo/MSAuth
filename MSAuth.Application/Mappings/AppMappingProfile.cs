using AutoMapper;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;

namespace MSAuth.Application.Mappings
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<App, AppCreateDTO>();
        }
    }
}
