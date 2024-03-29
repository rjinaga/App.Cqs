﻿namespace App.Cqs;


public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    TResult Handle(TCommand command);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    void Handle(TCommand command);
}