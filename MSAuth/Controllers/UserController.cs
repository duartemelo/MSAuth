using Microsoft.AspNetCore.Mvc;
using MSAuth.Application.DTOs;
using MSAuth.Application.Services;

namespace MSAuth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserGetDTO>> GetUserById(int userId)
        {
            // Retrieve the AppKey from the request headers
            if (!Request.Headers.TryGetValue("AppKey", out var appKey))
            {
                return BadRequest("AppKey not provided in headers.");
            }

            var user = await _userService.GetUserByIdAsync(userId, appKey.ToString());

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
