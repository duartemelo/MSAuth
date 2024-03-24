using MSAuth.Domain.DTOs;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Utils;

namespace MSAuth.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelErrorsContext _modelErrorsContext;
        public UserService(IUnitOfWork unitOfWork, ModelErrorsContext modelErrorsContext)
        {
            _unitOfWork = unitOfWork;
            _modelErrorsContext = modelErrorsContext;
        }

        public User? CreateUser(UserCreateDTO userToCreate, App app)
        {
            if (!ValidateUser(userToCreate)) return null;

            var user = new User(userToCreate.ExternalId, app, userToCreate.Email, userToCreate.Password);
            return _unitOfWork.UserRepository.Add(user);
        }

        private bool ValidateUser(UserCreateDTO userToCreate)
        {
            ValidateEmail(userToCreate.Email);
            ValidatePassword(userToCreate.Password);

            return !_modelErrorsContext.HasModelError(nameof(User));
        }

        private void ValidateEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _modelErrorsContext.AddModelError(nameof(User), nameof(email), "E-mail cannot be empty.");
            }
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                _modelErrorsContext.AddModelError(nameof(User), nameof(password), "The password cannot be empty.");
            }
            if (password.Length < 8)
            {
                _modelErrorsContext.AddModelError(nameof(User), nameof(password), "The password must have at least 8 characters.");
            }
            if (!PasswordValidation.ContainsUpperCaseLetter(password))
            {
                _modelErrorsContext.AddModelError(nameof(User), nameof(password), "The password must have at least 1 upcase letter.");
            }
            if (!PasswordValidation.ContainsDigit(password))
            {
                _modelErrorsContext.AddModelError(nameof(User), nameof(password), "The password must have at least 1 number.");
            }
        }
    }
}
