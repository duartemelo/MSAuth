using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Notifications;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using static MSAuth.Domain.Constants.Constants;

namespace MSAuth.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IValidator<UserCreateDTO> _userCreateDTOValidator;
        private readonly EntityValidationService _entityValidationService;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IValidator<UserCreateDTO> userCreateDTOValidator, ModelErrorsContext modelErrorsContext, EntityValidationService entityValidationService, UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IConfiguration configuration, NotificationContext notificationContext, IUnitOfWork unitOfWork)
        {
            _userCreateDTOValidator = userCreateDTOValidator;
            _modelErrorsContext = modelErrorsContext;
            _entityValidationService = entityValidationService;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> CreateUserAsync(UserCreateDTO userToCreate, App app)
        {
            var validationResult = _entityValidationService.Validate(_userCreateDTOValidator, userToCreate);
            if (!validationResult)
            {
                return null;
            }

            var userExists = await _unitOfWork.UserRepository.GetUserExistsSameApp(userToCreate.Email, app.AppKey);
            if (userExists)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_ALREADY_EXISTS, string.Empty);
                return null;
            }

            var user = new User(app, userToCreate.Email);

            var hashedPassword = _passwordHasher.HashPassword(user, userToCreate.Password);
            user.PasswordHash = hashedPassword;
            user.DateOfRegister = DateTime.Now;

            var result = await _userManager.CreateAsync(user, userToCreate.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelErrorsContext.AddModelError(typeof(UserCreateDTO).Name, error.Code, error.Description);
                }
            }

            return result.Succeeded ? user : null;
        }

        public string GenerateTokenString(User user)
        {
            IEnumerable<Claim> claims = new List<Claim> 
            {
                new(ClaimTypes.Email, user.Email!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value!));

            SigningCredentials signingCredential = new(
                securityKey,
                SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.Now.AddMinutes(Double.Parse(_configuration.GetSection("Jwt:ExpiresMin").Value!)),
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                signingCredentials:signingCredential);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public async Task<bool> ValidateUserIsConfirmed(User existentUser)
        {
            var confirmation = await _unitOfWork.UserConfirmationRepository.GetEntity().Where(x => x.User == existentUser && x.DateOfConfirm != null).FirstOrDefaultAsync();

            return confirmation != null;
        }
    }
}
