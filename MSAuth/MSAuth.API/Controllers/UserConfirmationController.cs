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
            bool result = await _userConfirmationAppService.Confirm(validation);
            return DomainResult<bool>.Ok(result, _notificationContext, _modelErrorsContext);
        }


        /// <summary>
        /// Creates a new validation token
        /// </summary>
        /// <param name="confirmationCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserConfirmationCreateDTO confirmationCreate) // TODO: change to user email's
        {
            string? result = await _userConfirmationAppService.Create(confirmationCreate);
            return DomainResult<string?>.Ok(result, _notificationContext, _modelErrorsContext);
        }
    }
}
