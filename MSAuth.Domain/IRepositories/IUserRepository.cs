﻿using MSAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId, string appKey);
        Task<User?> GetByEmailAsync(string email);
        Task<Boolean> GetUserExistsSameAppByEmail(string email, string appKey);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}