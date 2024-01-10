namespace App.Cqs.Tests.Helpers;

using App.Cqs;
using System.Threading.Tasks;


[Service]
public class CustomerCommandHandler : 
    ICommandHandlerAsync<CreateCustomerCommand, int>,
    ICommandHandler<CreateCustomerCommand, int>,
    ICommandHandlerAsync<UpdateCustomerCommand>,
    ICommandHandler<UpdateCustomerCommand>
{
    readonly IEventPublisher _publisher;
    public CustomerCommandHandler(IEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public int Handle(CreateCustomerCommand command)
    {
        var customer = command.Customer;

        // TODO: create customer record

        _publisher.Publish(new CustomerCreatedEvent(customer));
        return customer.Id;
    }
    public async Task<int> HandleAsync(CreateCustomerCommand command, CancellationToken token = default)
    {
        var customer = command.Customer;

        // TODO: create customer record

        await _publisher.PublishAsync(new CustomerCreatedEvent(customer));
        return customer.Id;
    }


    public void Handle(UpdateCustomerCommand command)
    {
        
    }

  
    public Task HandleAsync(UpdateCustomerCommand command, CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
}
