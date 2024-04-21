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
    public class AppController : Controller
    {
        private readonly IAppAppService _appService;
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;

        public AppController(IAppAppService appService, NotificationContext notificationContext, ModelErrorsContext modelErrorsContext)
        {
            _appService = appService;
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApp()
        {
            var app = await _appService.CreateAppAsync();
            return DomainResult<AppCreateDTO>.Ok(app, _notificationContext, _modelErrorsContext);
        }
    }
}
