namespace App.Cqs;

using Autofac;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IComponentContext _context;

    // Maintain dictionary of typeof ICommand<TResult> or ICommand and typeof ICommandHandlerAsync<,> key value pair
    private readonly ConcurrentDictionary<Type, Type> _cacheAsyncTypes;
    private readonly ConcurrentDictionary<Type, Type> _cacheTypes;

    public CommandDispatcher(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cacheAsyncTypes = new ConcurrentDictionary<Type, Type>();
        _cacheTypes = new ConcurrentDictionary<Type, Type>();
    }

    public TResult Dispatch<TResult>(ICommand<TResult> cmd)
    {
        Type cmdType = cmd.GetType();
        Type cmdHandlerType;

        if (!_cacheTypes.ContainsKey(cmdType))
        {
            cmdHandlerType = typeof(ICommandHandler<,>).MakeGenericType(cmdType, typeof(TResult));
            _ = _cacheTypes.TryAdd(cmdType, cmdHandlerType);
        }
        else
        {
            cmdHandlerType = _cacheTypes[cmdType];
        }

        dynamic handler = _context.Resolve(cmdHandlerType);
        dynamic arg = cmd;
        var result = handler.Handle(arg);

        return result;
    }

    public void Dispatch(ICommand cmd)
    {
        Type cmdType = cmd.GetType();
        Type cmdHandlerType;

        if (!_cacheTypes.ContainsKey(cmdType))
        {
            cmdHandlerType = typeof(ICommandHandler<>).MakeGenericType(cmdType);
            _ = _cacheTypes.TryAdd(cmdType, cmdHandlerType);
        }
        else
        {
            cmdHandlerType = _cacheTypes[cmdType];
        }

        dynamic handler = _context.Resolve(cmdHandlerType);
        dynamic arg = cmd;
        handler.HandleAsync(arg);
    }

    public Task<TResult> DispatchAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default)
    {
        Type cmdType = cmd.GetType();
        Type cmdHandlerType;

        if (!_cacheAsyncTypes.ContainsKey(cmdType))
        {
            cmdHandlerType = typeof(ICommandHandlerAsync<,>).MakeGenericType(cmdType, typeof(TResult));
            _ = _cacheAsyncTypes.TryAdd(cmdType, cmdHandlerType);
        }
        else
        {
            cmdHandlerType = _cacheAsyncTypes[cmdType];
        }

        dynamic handler = _context.Resolve(cmdHandlerType);
        dynamic arg = cmd;
        var taskResult = handler.HandleAsync(arg, cancellationToken);

        return taskResult;
    }

    public Task DispatchAsync(ICommand cmd, CancellationToken cancellationToken = default)
    {
        Type cmdType = cmd.GetType();
        Type cmdHandlerType;

        if (!_cacheAsyncTypes.ContainsKey(cmdType))
        {
            cmdHandlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(cmdType);
            _ = _cacheAsyncTypes.TryAdd(cmdType, cmdHandlerType);
        }
        else
        {
            cmdHandlerType = _cacheAsyncTypes[cmdType];
        }

        dynamic handler = _context.Resolve(cmdHandlerType);
        dynamic arg = cmd;
        var taskResult = handler.HandleAsync(arg, cancellationToken);

        return taskResult;
    }
}