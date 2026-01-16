using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.Log;

internal class Module : ModuleBase
{
    public override Task RegisterAsync(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(FooLogger<>)).As(typeof(IAppLogger<>));
        return base.RegisterAsync(builder);
    }

    public override async Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterRootMainMenuItem<LogMainMenuItem>();
        await Task.CompletedTask;
    }
}