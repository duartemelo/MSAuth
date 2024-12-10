using Microsoft.AspNetCore.Mvc;
using MSGym.API.Extensions;
using MSGym.Domain.DTOs;
using MSGym.Domain.ModelErrors;
using MSGym.Domain.Notifications;

namespace MSGym.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymController : Controller
    {
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;

        public GymController(NotificationContext notificationContext, ModelErrorsContext modelErrorsContext)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostGym(GymCreateDTO gym)
        {
            //var createdGym = await _gymAppService.CreateGymAsync(gym);

            var createdGym = new GymCreateDTO
            {
                Name = "test",
                Address = "test",
                ZipCode = "test",
                Email = "test"
            };

            // TODO: create the gym
            // create the role admin for this gym
            // associate this user with the role

            return DomainResult<GymCreateDTO?>.Ok(createdGym, _notificationContext, _modelErrorsContext);
        }
    }
}
