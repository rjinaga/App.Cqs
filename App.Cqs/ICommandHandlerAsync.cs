﻿

namespace App.Cqs;

using System.Threading.Tasks;

public interface ICommandHandlerAsync<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> HandleAsync(TCommand command, CancellationToken token = default);
}

public interface ICommandHandlerAsync<in TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken token = default);
}
