using FluentValidation.Results;
using MSAuth.Domain.Entities;
using MSAuth.Domain.Interfaces.Services;
using MSAuth.Domain.Interfaces.UnitOfWork;

namespace MSAuth.Domain.Services
{
    public class UserConfirmationService : IUserConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserConfirmationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserConfirmation? CreateUserConfirmation(User user)
        {
            var userConfirmation = new UserConfirmation(user);
            return _unitOfWork.UserConfirmationRepository.Add(userConfirmation);
        }

    }
}
