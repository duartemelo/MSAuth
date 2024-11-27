using MassTransit;
using MSGym.Domain.DTOs;
using MSGym.Domain.Interfaces.Services;
using SharedEvents.User;

namespace MSGym.Application.Consumers
{
    public class CreateUserConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly IUserService _userService;
        public CreateUserConsumer(IUserService userService) 
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var userCreatedEvent = context.Message;

            await _userService.CreateUserAsync(new UserCreateDTO
            {
                ExternalId = userCreatedEvent.UserId,
                Email = userCreatedEvent.Email,
                FirstName = userCreatedEvent.FirstName,
                LastName = userCreatedEvent.LastName,
                PhoneNumber = userCreatedEvent.PhoneNumber
            });
        }
    }
}
