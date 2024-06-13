﻿using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> CreateUserAsync(UserCreateDTO userToCreate, App app);
        string GenerateTokenString(User existentUser);
    }
}