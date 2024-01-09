namespace App.Cqs;

public class CqDispatcher : IDispatcher
{
    readonly IQueryDispatcher _queryDispatcher;
    readonly ICommandDispatcher _commandDispatcher;

    public CqDispatcher(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    public TResult Dispatch<TResult>(ICommand<TResult> cmd)
    {
        return _commandDispatcher.Dispatch<TResult>(cmd);
    }

    public void Dispatch(ICommand cmd)
    {
        _commandDispatcher.Dispatch(cmd);
    }

    public TResult Dispatch<TResult>(IQuery<TResult> query)
    {
        return _queryDispatcher.Dispatch(query);
    }

    public Task<TResult> DispatchAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default)
    {
        return _commandDispatcher.DispatchAsync(cmd, cancellationToken);
    }

    public Task DispatchAsync(ICommand cmd, CancellationToken cancellationToken = default)
    {
        return _commandDispatcher.DispatchAsync(cmd, cancellationToken);
    }

    public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return _queryDispatcher.DispatchAsync(query, cancellationToken);
    }
}
