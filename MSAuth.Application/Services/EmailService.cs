using Hangfire;
using Microsoft.Extensions.Configuration.UserSecrets;
using MSAuth.Domain.Entities;
using MSAuth.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSAuth.Application.Services
{
    public class EmailService
    {
        private readonly IUserConfirmationRepository _userConfirmationRepository;
        private readonly IUserRepository _userRepository;

        public EmailService(IUserConfirmationRepository userConfirmationRepository, IUserRepository userRepository)
        {
            _userConfirmationRepository = userConfirmationRepository;
            _userRepository = userRepository;
        }

        public async Task SendUserConfirmationJob(int userId, string appKey)
        {
            var user = await _userRepository.GetByIdAsync(userId, appKey);

            if (user != null && user.Email != null)
            {
                var emailResult = await SendEmailMock(user.Email);
                if (emailResult == true)
                {
                    var userConfirmationToCreate = new UserConfirmation(user);
                    await _userConfirmationRepository.AddAsync(userConfirmationToCreate);  
                }
            }
        }

        private static async Task<bool> SendEmailMock(string userEmail)
        {
            await Task.Delay(40000);
            Console.WriteLine("Email was sent! " + userEmail);
            return true;
        }
    }
}
