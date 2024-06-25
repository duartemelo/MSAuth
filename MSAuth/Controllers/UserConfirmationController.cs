using Microsoft.AspNetCore.Mvc;
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
    public class UserConfirmationController : Controller
    {
        private readonly IUserConfirmationAppService _userConfirmationAppService;
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;
        public UserConfirmationController(NotificationContext notificationContext, ModelErrorsContext modelErrorsContext, IUserConfirmationAppService userConfirmationAppService)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            _userConfirmationAppService = userConfirmationAppService;
        }

        /// <summary>
        /// Validates a user confirmation
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("Confirm")]
        public async Task<IActionResult> ConfirmUser(UserConfirmationValidateDTO validation)
        {
            bool result = await _userConfirmationAppService.Confirm(validation, AppKey.GetAppKey(HttpContext));
            return DomainResult<bool>.Ok(result, _notificationContext, _modelErrorsContext);
        }


        /// <summary>
        /// Creates a new validation token
        /// </summary>
        /// <param name="confirmationCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserConfirmationCreateDTO confirmationCreate)
        {
            string? result = await _userConfirmationAppService.Create(confirmationCreate, AppKey.GetAppKey(HttpContext));
            return DomainResult<string?>.Ok(result, _notificationContext, _modelErrorsContext);
        }
    }
}
