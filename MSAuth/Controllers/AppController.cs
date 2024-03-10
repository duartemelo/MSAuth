using Microsoft.AspNetCore.Mvc;
using MSAuth.API.Extensions;
using MSAuth.Application.DTOs;
using MSAuth.Application.Services;
using MSAuth.Domain.Notifications;

namespace MSAuth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppController : Controller
    {
        private readonly AppService _appService;
        private readonly NotificationContext _notificationContext;

        public AppController(AppService appService, NotificationContext notificationContext)
        {
            _appService = appService;
            _notificationContext = notificationContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApp()
        {
            var app = await _appService.CreateAppAsync();
            return DomainResult<AppCreateDTO>.Ok(app, _notificationContext);
        }
    }
}
