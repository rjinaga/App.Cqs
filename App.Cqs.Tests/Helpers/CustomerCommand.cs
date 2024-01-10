namespace App.Cqs.Tests.Helpers;

using App.Cqs;


public record CreateCustomerCommand (Customer Customer) : ICommand<int>
{
    
}

public record UpdateCustomerCommand(Customer Customer) : ICommand
{

}

