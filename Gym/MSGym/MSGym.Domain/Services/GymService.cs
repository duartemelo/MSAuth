using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MSGym.Domain.DTOs;
using MSGym.Domain.Entities;
using MSGym.Domain.Interfaces.Services;
using MSGym.Domain.Interfaces.UnitOfWork;
using MSGym.Domain.Notifications;
using static MSGym.Domain.Constants.Constants;

namespace MSGym.Domain.Services
{
    public class GymService : IGymService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityValidationService _entityValidationService;
        private readonly IValidator<GymCreateDTO> _gymCreateDTOValidator;
        private readonly NotificationContext _notificationContext;

        public GymService(IUnitOfWork unitOfWork, EntityValidationService entityValidationService, IValidator<GymCreateDTO> gymCreateDTOValidator, NotificationContext notificationContext)
        {
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _gymCreateDTOValidator = gymCreateDTOValidator;
            _notificationContext = notificationContext;
        }

        public async Task<Gym?> CreateGymAsync(GymCreateDTO gymToCreate)
        {
            var validationResult = _entityValidationService.Validate(_gymCreateDTOValidator, gymToCreate);
            if (!validationResult)
            {
                return null;
            }

            var gymExists = await _unitOfWork.GymRepository
                .GetEntity()
                .AnyAsync(x => x.Name == gymToCreate.Name || x.Email == gymToCreate.Email);

            if (gymExists)
            {
                _notificationContext.AddNotification(NotificationKeys.GYM_ALREADY_EXISTS);
                return null;
            }

            var userEmail = "postman2@temp.pt"; // TODO: gather user (how to get it from JWT?)

            var user = await _unitOfWork.UserRepository.GetEntity().FirstOrDefaultAsync(x => x.Email == userEmail);

            if (user == null)
            {
                _notificationContext.AddNotification(NotificationKeys.USER_NOT_FOUND);
                return null;
            }

            var gym = new Gym(gymToCreate.Name, gymToCreate.Address, gymToCreate.ZipCode, gymToCreate.Email, user);

            var createdGym = await _unitOfWork.GymRepository.AddAsync(gym);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR);
                return null;
            }

            return createdGym;
        }

        public async Task<bool> DeleteGymAsync(long id, string requestUserEmail)
        {
            var gym = await _unitOfWork.GymRepository
                .GetEntity()
                .Include(x => x.Roles)
                    .ThenInclude(x => x.TrainingPlans) // if there are training plans, gym cannot be deleted
                .FirstOrDefaultAsync(x => x.Id == id);

            if (gym == null)
            {
                _notificationContext.AddNotification("GYM_NOT_FOUND"); // TODO: constants
                return false;
            }

            if (gym.CreationUserEmail != requestUserEmail)
            {
                _notificationContext.AddNotification("USER_HAS_NO_PERMISSION_TO_DELETE_GYM"); // TODO: constants
                return false;
            }

            if (gym.IsDeletable() == false)
            {
                _notificationContext.AddNotification("GYM_IS_NOT_DELETABLE");
                return false;
            }

            _unitOfWork.GymRepository.Delete(gym);

            if (!await _unitOfWork.CommitAsync())
            {
                _notificationContext.AddNotification(NotificationKeys.DATABASE_COMMIT_ERROR);
                return false;
            }

            return true;
        }
    }
}
