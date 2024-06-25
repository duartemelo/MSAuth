using AutoMapper;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;

namespace MSAuth.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() {
            CreateMap<User, UserGetDTO>();
            CreateMap<User, UserCreateResponseDTO>();
        }
    }
}
