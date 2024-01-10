namespace App.Cqs.Tests.Helpers;

using App.Cqs;
using System.Threading.Tasks;

[Service]
public class CustomerCreatedEventHandler : 
    IEventHandlerAsync<CustomerCreatedEvent, Customer>,
    IEventHandler<CustomerCreatedEvent, Customer>
{
    public void Handle(Customer arg)
    {
        arg.EventTestPurposeAttr = "Handle";
    }

    public Task HandleAsync(Customer arg, CancellationToken token = default)
    {
        arg.EventTestPurposeAttr = "HandleAsync";
        return Task.CompletedTask;
    }
}
