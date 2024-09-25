using Microsoft.AspNetCore.Mvc;
using MSAuth.API.Extensions;
using MSAuth.Application.Interfaces;
using MSAuth.Domain.DTOs;
using MSAuth.Domain.ModelErrors;
using MSAuth.Domain.Notifications;

namespace MSAuth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAppService _userAppService;
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;
        public UserController(IUserAppService userAppService, NotificationContext notificationContext, ModelErrorsContext modelErrorsContext)
        {
            _userAppService = userAppService;
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
        }

        [HttpGet("InternalId/{id}")]
        public async Task<IActionResult> GetUserByInternalId(long id)
        {
            var user = await _userAppService.GetUserByIdAsync(id);
            return DomainResult<UserGetDTO?>.Ok(user, _notificationContext, _modelErrorsContext);         
        }

        /// <summary>
        /// Generates JWT token based on user email and password
        /// </summary>
        /// <param name="user">User email and password</param>
        /// <returns>JWT Token</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserLoginDTO user)
        {
            var result = await _userAppService.Login(user);
            return DomainResult<UserLoginResponseDTO?>.Ok(result, _notificationContext, _modelErrorsContext);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostUser(UserCreateDTO user)
        {
            var createdUser = await _userAppService.CreateUserAsync(user);
            return DomainResult<UserCreateResponseDTO?>.Ok(createdUser, _notificationContext, _modelErrorsContext);
        }

        // TODO: add delete user
        // brainstorm on this, how to deal with added stuff on MSGym?
        // produce event to delete

        [HttpGet("Refresh/{refreshToken}")]
        public async Task<IActionResult> RefreshUser(string refreshToken)
        {
            var result = await _userAppService.Refresh(refreshToken);
            return DomainResult<UserLoginResponseDTO?>.Ok(result, _notificationContext, _modelErrorsContext);
        }
    }
}
