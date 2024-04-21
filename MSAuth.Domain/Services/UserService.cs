using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;

namespace MSAuth.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IValidator<UserCreateDTO> _userCreateDTOValidator;
        private readonly EntityValidationService _entityValidationService;
        private readonly UserManager<User> _userManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ModelErrorsContext _modelErrorsContext;

        public UserService(IValidator<UserCreateDTO> userCreateDTOValidator, ModelErrorsContext modelErrorsContext, EntityValidationService entityValidationService, UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
        {
            _userCreateDTOValidator = userCreateDTOValidator;
            _modelErrorsContext = modelErrorsContext;
            _entityValidationService = entityValidationService;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> CreateUserAsync(UserCreateDTO userToCreate, App app)
        {
            var validationResult = _entityValidationService.Validate(_userCreateDTOValidator, userToCreate);
            if (!validationResult)
            {
                return null;
            }

            var user = new User(userToCreate.ExternalId, app, userToCreate.Email);

            var hashedPassword = _passwordHasher.HashPassword(user, userToCreate.Password);
            user.PasswordHash = hashedPassword;
            user.DateOfRegister = DateTime.Now;

            var result = await _userManager.CreateAsync(user, userToCreate.Password);

            // TODO: fix? refactor?
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelErrorsContext.AddModelError(typeof(UserCreateDTO).Name, error.Code, error.Description);
                }
            }

            return result.Succeeded ? user : null;
        }
    }
}
