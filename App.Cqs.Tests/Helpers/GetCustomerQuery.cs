namespace App.Cqs.Tests.Helpers;

using App.Cqs;


public record GetCustomerQuery(int CustomerId) : IQuery<Customer>
{
}