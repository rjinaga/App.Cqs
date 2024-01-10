namespace App.Cqs;

public interface IEventHandler<TEvent, TArg> 
    where TEvent : IEvent<TArg> 
    where TArg : class
{
    void Handle(TArg arg);
}
