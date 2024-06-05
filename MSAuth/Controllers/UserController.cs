﻿using Microsoft.AspNetCore.Mvc;
using MSAuth.API.ActionFilters;
using MSAuth.API.Extensions;
using MSAuth.API.Utils;
using MSAuth.Application.Interfaces;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Notifications;

namespace MSAuth.API.Controllers
{
    [ApiController]
    [RequireAppKey]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAppService _userService;
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;
        public UserController(IUserAppService userService, NotificationContext notificationContext, ModelErrorsContext modelErrorsContext)
        {
            _userService = userService;
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
        }

        [HttpGet("InternalId/{id}")]
        public async Task<IActionResult> GetUserByInternalId(string id)
        {
            var user = await _userService.GetUserByIdAsync(id, AppKey.GetAppKey(HttpContext));
            return DomainResult<UserGetDTO?>.Ok(user, _notificationContext, _modelErrorsContext);         
        }

        [HttpGet("ExternalId/{id}")]
        public async Task<IActionResult> GetUserByExternalId(string id)
        {
            var user = await _userService.GetUserByExternalIdAsync(id, AppKey.GetAppKey(HttpContext));
            return DomainResult<UserGetDTO?>.Ok(user, _notificationContext, _modelErrorsContext);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostUser(UserCreateDTO user)
        {
            var createdUser = await _userService.CreateUserAsync(user, AppKey.GetAppKey(HttpContext));
            return DomainResult<UserGetDTO?>.Ok(createdUser, _notificationContext, _modelErrorsContext);
        }
    }
}
