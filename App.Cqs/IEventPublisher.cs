namespace App.Cqs;

public interface IEventPublisher
{
    Task PublishAsync<TArg>(IEvent<TArg> @event, CancellationToken token=default) where TArg : class;
    void Publish<TArg>(IEvent<TArg> @event) where TArg : class;
}
