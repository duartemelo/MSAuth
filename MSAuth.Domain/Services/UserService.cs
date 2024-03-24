using FluentValidation;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;

namespace MSAuth.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserCreateDTO> _userCreateDTOValidator;
        private readonly EntityValidationService _entityValidationService;
        public UserService(IUnitOfWork unitOfWork, IValidator<UserCreateDTO> userCreateDTOValidator,  ModelErrorsContext modelErrorsContext, EntityValidationService entityValidationService)
        {
            _unitOfWork = unitOfWork;
            _userCreateDTOValidator = userCreateDTOValidator;
            _entityValidationService = entityValidationService;
        }

        public User? CreateUser(UserCreateDTO userToCreate, App app)
        {
            var validationResult = _entityValidationService.Validate(_userCreateDTOValidator, userToCreate);
            if (!validationResult)
            {
                return null;
            }
            var user = new User(userToCreate.ExternalId, app, userToCreate.Email, userToCreate.Password);
            return _unitOfWork.UserRepository.Add(user);
        }
    }
}
