using AutoMapper;
using Microsoft.AspNetCore.Http;
using MSGym.Application.Interfaces;
using MSGym.Domain.DTOs;
using MSGym.Domain.Interfaces.Services;
using MSGym.Domain.Notifications;
using System.Security.Claims;

namespace MSGym.Application.Services
{
    public class GymAppService : IGymAppService
    {
        private readonly IGymService _gymService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NotificationContext _notificationContext;

        public GymAppService(IGymService gymService, IMapper mapper, IHttpContextAccessor httpContextAccessor, NotificationContext notificationContext)
        {
            _gymService = gymService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _notificationContext = notificationContext;
        }

        public async Task<GymCreateDTO?> CreateGymAsync(GymCreateDTO gymToCreate)
        {
            var createdGym = await _gymService.CreateGymAsync(gymToCreate);
            return _mapper.Map<GymCreateDTO>(createdGym);
        }

        public async Task<bool> DeleteGymAsync(long id)
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail == null)
            {
                _notificationContext.AddNotification("NO_EMAIL_FOUND_ON_CLAIM"); // TODO: constants
                return false;
            }

            return await _gymService.DeleteGymAsync(id, userEmail);
        }
    }
}
