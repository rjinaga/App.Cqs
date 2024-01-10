namespace App.Cqs.Tests;

using Autofac;

public class TestBase
{
    protected readonly IContainer Container;
    public TestBase()
    {
        var builder = new ContainerBuilder();
        builder.RegisterAssemblyTypesWithServiceAttr(this.GetType().Assembly);
        builder.RegisterCqs();
        Container = builder.Build();
    }
}
