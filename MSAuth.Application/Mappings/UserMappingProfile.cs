using AutoMapper;
using MSAuth.Application.DTOs;
using MSAuth.Domain.Entities;

namespace MSAuth.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() {
            CreateMap<User, UserGetDTO>();
        }
    }
}
