using MSAuth.Application.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserGetDTO?> GetUserByIdAsync(int userId, string appKey)
        {
            var user = await _userRepository.GetByIdAsync(userId, appKey);

            return user != null
                ? new UserGetDTO
                {
                    Id = user.Id,
                    ExternalId = user.ExternalId,
                    Email = user.Email,
                    DateOfRegister = user.DateOfRegister,
                    DateOfModification = user.DateOfModification,
                    DateOfLastAccess = user.DateOfLastAccess,
                }
                : null;
        }
    }
}
