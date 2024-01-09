

namespace App.Cqs;

using System.Threading.Tasks;

public interface ICommandDispatcher
{
    Task<TResult> DispatchAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default);
    Task DispatchAsync(ICommand cmd, CancellationToken cancellationToken = default);
    TResult Dispatch<TResult>(ICommand<TResult> cmd);
    void Dispatch(ICommand cmd);
}