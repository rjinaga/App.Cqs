﻿

namespace App.Cqs;


public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    TResponse Handle(TQuery query);
}