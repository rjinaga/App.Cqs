namespace App.Cqs.Tests.Helpers;

using App.Cqs;

public record CustomerCreatedEvent(Customer Arg) : IEvent<Customer>
{
}
