using Microsoft.AspNetCore.Mvc;
using MSAuth.API.ActionFilters;
using MSAuth.Application.DTOs;
using MSAuth.Application.Services;

namespace MSAuth.API.Controllers
{
    [ApiController]
    [RequireAppKey]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        private string? GetAppKey()
        {
            string? appKey = HttpContext.Items["AppKey"]?.ToString();

            if (appKey == null)
            {
                return null;
            }

            return appKey;
        } 

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserGetDTO>> GetUserById(int userId)
        {
            var appKey = GetAppKey();
            if (appKey == null)
            {
                return BadRequest();
            }

            var user = await _userService.GetUserByIdAsync(userId, appKey);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
