namespace App.Cqs.Tests;

using Autofac;

public class CommandDispatcherTest : TestBase
{
    [Fact]
    public void DispatchCommand_NoException()
    {
        // Arrange
        var disparcher = Container.Resolve<ICommandDispatcher>();
        var command = new Helpers.CreateCustomerCommand(new Customer());

        // Act -- This tests event handlers too
        var result = Record.Exception(() => disparcher.Dispatch(command));

        // Assert
        Assert.Null(result);
        Assert.Equal("Handle", command.Customer.EventTestPurposeAttr);
    }

    [Fact]
    public async void DispatchAsyncCommand_NoException()
    {
        // Arrange
        var disparcher = Container.Resolve<ICommandDispatcher>();
        var command = new Helpers.CreateCustomerCommand(new Customer());

        // Act -- This tests event handlers too
        var result = await Record.ExceptionAsync(async () => await disparcher.DispatchAsync(command));

        // Assert
        Assert.Null(result);
        Assert.Equal("HandleAsync", command.Customer.EventTestPurposeAttr);
    }

    [Fact]
    public void DispatchCommandWihtoutResult_NoException()
    {
        // Arrange
        var disparcher = Container.Resolve<ICommandDispatcher>();
        var command = new Helpers.UpdateCustomerCommand(new Customer());

        // Act
        var result = Record.Exception(() => disparcher.Dispatch(command));

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async void DispatchAsyncCommandWithoutResult_NoException()
    {
        // Arrange
        var disparcher = Container.Resolve<ICommandDispatcher>();
        var command = new Helpers.UpdateCustomerCommand(new Customer());

        // Act
        var result = await Record.ExceptionAsync(async () => await disparcher.DispatchAsync(command));

        // Assert
        Assert.Null(result);
    }

}