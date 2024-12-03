namespace MSAuth.Application.Interfaces.Infrastructure.Communication
{
    public interface IEventProducer
    {
        Task PublishAsync<T>(T @event) where T : class;
    }
}
