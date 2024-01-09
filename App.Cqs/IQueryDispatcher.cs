namespace App.Cqs;

using System.Threading.Tasks;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    TResult Dispatch<TResult>(IQuery<TResult> query);
}
