using MassTransit;
using MSAuth.Application.Interfaces.Infrastructure.Communication;

namespace MSAuth.Infrastructure.Communication
{
    public class EventProducer : IEventProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T @event) where T : class
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}
