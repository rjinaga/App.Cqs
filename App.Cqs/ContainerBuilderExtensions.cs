namespace App.Cqs;

using Autofac;
using System.Linq;
using System.Reflection;


public static class ContainerBuilderExtensions
{
    public static void RegisterCqs(this ContainerBuilder builder)
    {
        builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().SingleInstance();
        builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().SingleInstance();
        builder.RegisterType<CqDispatcher>().As<IDispatcher>().SingleInstance();
        builder.RegisterType<EventPublisher>().As<IEventPublisher>().SingleInstance();
    }

    public static void RegisterAssemblyTypesWithServiceAttr(this ContainerBuilder builder, Assembly assembly)
    {
        // :Register transient services
        builder.RegisterAssemblyTypes(assembly)
           .Where(t =>
           {
               var sa = t.GetCustomAttribute<ServiceAttribute>();
               return sa != null && sa.InstanceLifetime == InstanceLifetime.Transient;
           })
           .AsImplementedInterfaces();

        // :Register singleton services
        builder.RegisterAssemblyTypes(assembly)
           .Where(t =>
           {
               var sa = t.GetCustomAttribute<ServiceAttribute>();
               return sa != null && sa.InstanceLifetime == InstanceLifetime.Singleton;
           })
           .AsImplementedInterfaces()
           .SingleInstance();
    }
}
