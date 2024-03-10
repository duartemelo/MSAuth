using MSAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int userId, string appKey);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> GetUserExistsSameAppByEmail(string email, string appKey);
        User Add(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
