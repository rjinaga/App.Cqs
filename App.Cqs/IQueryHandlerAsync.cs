﻿

namespace App.Cqs;

using System.Threading.Tasks;
public interface IQueryHandlerAsync<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<TResponse> HandleAsync(TQuery query, CancellationToken token=default);
}