namespace MSAuth.Domain.Interfaces.Communication
{
    public interface IEventProducer
    {
        Task PublishAsync<T>(T @event) where T : class;
    }
}
