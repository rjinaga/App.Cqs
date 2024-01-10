namespace App.Cqs.Tests;

using Autofac;

public class QueryDispatcherTest : TestBase
{
    [Fact]
    public void DispatchQuery_NoException()
    {
        // Arrange
        var disparcher = Container.Resolve<IQueryDispatcher>();
        var query = new Helpers.GetCustomerQuery(1);

        // Act -- This tests event handlers too
        var result = Record.Exception(() => disparcher.Dispatch(query));

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async void DispatchQueryAsync_NoException()
    {
        // Arrange
        var disparcher = Container.Resolve<IQueryDispatcher>();
        var query = new Helpers.GetCustomerQuery(1);

        // Act -- This tests event handlers too
        var result = await Record.ExceptionAsync(async () => await disparcher.DispatchAsync(query));

        // Assert
        Assert.Null(result);
    }
}
