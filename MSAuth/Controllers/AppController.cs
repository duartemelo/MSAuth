using Microsoft.AspNetCore.Mvc;
using MSAuth.Application.DTOs;
using MSAuth.Application.Services;

namespace MSAuth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppController : Controller
    {
        private readonly AppService _appService;

        public AppController(AppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        public async Task<ActionResult<AppCreateDTO>> CreateApp()
        {
            var app = await _appService.CreateAppAsync();
            return Ok(app);
        }
    }
}
