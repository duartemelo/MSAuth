using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Services
{
    public interface IUserService
    {
        User? CreateUser(UserCreateDTO userToCreate, App app);
    }
}