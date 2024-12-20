﻿using Microsoft.AspNetCore.Mvc;
using MSGym.API.Extensions;
using MSGym.Application.Interfaces;
using MSGym.Domain.DTOs;
using MSGym.Domain.ModelErrors;
using MSGym.Domain.Notifications;

namespace MSGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymController : Controller
    {
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;
        private readonly IGymAppService _gymAppService;

        public GymController(NotificationContext notificationContext, ModelErrorsContext modelErrorsContext, IGymAppService gymAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _gymAppService = gymAppService;
        }

        [HttpPost]
        public async Task<IActionResult> PostGym(GymCreateDTO gym)
        {
            var createdGym = await _gymAppService.CreateGymAsync(gym);
            return DomainResult<GymCreateDTO?>.Ok(createdGym, _notificationContext, _modelErrorsContext);
        }
    }
}
