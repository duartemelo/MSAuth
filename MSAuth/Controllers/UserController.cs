using Microsoft.AspNetCore.Mvc;
using MSAuth.API.ActionFilters;
using MSAuth.API.Extensions;
using MSAuth.API.Utils;
using MSAuth.Application.DTOs;
using MSAuth.Application.Services;
using MSAuth.Domain.Notifications;

namespace MSAuth.API.Controllers
{
    [ApiController]
    [RequireAppKey]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly NotificationContext _notificationContext;
        public UserController(UserService userService, NotificationContext notificationContext)
        {
            _userService = userService;
            _notificationContext = notificationContext;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId, AppKey.GetAppKey(HttpContext));
            return DomainResult<UserGetDTO?>.Ok(user, _notificationContext);         
        }

        // TODO: GetUserByExternalId

        [HttpPost]
        public async Task<IActionResult> PostUser(UserCreateDTO user)
        {
            var createdUser = await _userService.CreateUserAsync(user, AppKey.GetAppKey(HttpContext));
            return DomainResult<UserGetDTO?>.Ok(createdUser, _notificationContext);
        }
    }
}
