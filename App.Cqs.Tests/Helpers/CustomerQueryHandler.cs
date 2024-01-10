namespace App.Cqs.Tests.Helpers;

using App.Cqs;
using System.Threading.Tasks;

[Service]
public class CustomerQueryHandler : 
    IQueryHandlerAsync<GetCustomerQuery, Customer>,
    IQueryHandler<GetCustomerQuery, Customer>
{
    
    public CustomerQueryHandler()
    {
     
    }

    public Customer Handle(GetCustomerQuery query)
    {
        return new Customer();
    }

    public Task<Customer> HandleAsync(GetCustomerQuery query, CancellationToken token = default)
    {
        return Task.FromResult(new Customer());
    }
}
