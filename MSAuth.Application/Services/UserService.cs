using AutoMapper;
using Hangfire;
using MSAuth.Application.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.IRepositories;

namespace MSAuth.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppRepository _appRepository;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IAppRepository appRepository, EmailService emailService, IMapper mapper)
        {
            _userRepository = userRepository;
            _appRepository = appRepository;
            _emailService = emailService;
            _mapper = mapper;
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

        public async Task<UserGetDTO?> CreateUserAsync(UserCreateDTO user, string appKey)
        {
            // TODO: validar se existem dois users com o mesmo email na mesma aplicaçao!
            var app = await _appRepository.GetByAppKeyAsync(appKey);
            if (app == null)
            {
                // TODO: mandar erro de app not found?
                return null; 
            }

            var userExists = await _userRepository.GetUserExistsSameAppByEmail(user.Email, appKey);
            if (userExists)
            {
                // TODO: handling errors
                // https://youtu.be/H3EbflpXVmo
                return null;
            }

            // TODO: try new user, se vier erro, handling
            var userToCreate = new User(user.ExternalId, app, user.Email, user.Password);

            await _userRepository.AddAsync(userToCreate);

            // simulating sending an email
            BackgroundJob.Enqueue(() => _emailService.SendUserConfirmationJob(userToCreate.Id, appKey));
            Console.WriteLine("Job was sent to queue!");

            return _mapper.Map<UserGetDTO>(userToCreate);
        }
    }
}
