using Autofac;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.BlazorDemo;

internal class Module : ModuleBase
{
    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<BlazorDemoTabItemFactory>();
        lifetimeScope.RegisterRootMainMenuItem<BlazorDemoMainMenuItem>();

        return Task.CompletedTask;
    }
}