namespace App.Cqs;

using Autofac;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IComponentContext _context;

    // Maintain dictionary of typeof IQuery<TResult> and typeof IQueryHandlerAsync<,> key value pair
    private readonly ConcurrentDictionary<Type, Type> _cacheAsyncTypes;
    private readonly ConcurrentDictionary<Type, Type> _cacheTypes;

    public QueryDispatcher(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cacheAsyncTypes = new ConcurrentDictionary<Type, Type>();
        _cacheTypes = new ConcurrentDictionary<Type, Type>();
    }

    public TResult Dispatch<TResult>(IQuery<TResult> query)
    {
        Type queryType = query.GetType();
        Type queryHandlerType;

        if (!_cacheTypes.ContainsKey(queryType))
        {
            queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
            _ = _cacheTypes.TryAdd(queryType, queryHandlerType);
        }
        else
        {
            queryHandlerType = _cacheTypes[queryType];
        }

        dynamic handler = _context.Resolve(queryHandlerType);
        dynamic arg = query;
        var result = handler.Handle(arg);

        return result;
    }

    public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        Type queryType = query.GetType();
        Type queryHandlerType;

        if (!_cacheAsyncTypes.ContainsKey(queryType))
        {
            queryHandlerType = typeof(IQueryHandlerAsync<, >).MakeGenericType(queryType, typeof(TResult));
            _ = _cacheAsyncTypes.TryAdd(queryType, queryHandlerType);
        }
        else
        {
            queryHandlerType = _cacheAsyncTypes[queryType];
        }

        dynamic handler = _context.Resolve(queryHandlerType);
        dynamic arg = query;
        var result = handler.HandleAsync(arg, cancellationToken);

        return result;
    }
}
