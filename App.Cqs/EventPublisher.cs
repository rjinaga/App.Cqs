namespace App.Cqs;

using Autofac;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class EventPublisher : IEventPublisher
{
    private readonly IComponentContext _context;

    // Maintain dictionary of typeof IEvent<TArg> and typeof IEventHandlerAsync<,>
    private readonly ConcurrentDictionary<Type, Type> _cacheAsyncTypes;
    private readonly ConcurrentDictionary<Type, Type> _cacheTypes;

    public EventPublisher(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cacheAsyncTypes = new ConcurrentDictionary<Type, Type>();
        _cacheTypes = new ConcurrentDictionary<Type, Type>();
    }

    public void Publish<TArg>(IEvent<TArg> @event) where TArg : class
    {
        Type eventType = @event.GetType();
        Type eventHandlerType;

        if (!_cacheAsyncTypes.ContainsKey(eventType))
        {
            eventHandlerType = typeof(IEventHandler<,>).MakeGenericType(eventType, typeof(TArg));
            _ = _cacheAsyncTypes.TryAdd(eventType, eventHandlerType);
        }
        else
        {
            eventHandlerType = _cacheAsyncTypes[eventType];
        }

        dynamic handler = _context.Resolve(eventHandlerType);
        handler.Handle(@event.Arg);
    }

    public async Task PublishAsync<TArg>(IEvent<TArg> @event, CancellationToken token=default) where TArg : class
    {
        Type eventType = @event.GetType();
        Type eventHandlerType;

        if (!_cacheAsyncTypes.ContainsKey(eventType))
        {
            eventHandlerType = typeof(IEventHandlerAsync<,>).MakeGenericType(eventType, typeof(TArg));
            _ = _cacheAsyncTypes.TryAdd(eventType, eventHandlerType);
        }
        else
        {
            eventHandlerType = _cacheAsyncTypes[eventType];
        }
        
        dynamic handler = _context.Resolve(eventHandlerType);
        await handler.HandleAsync(@event.Arg, token);
    }
}
