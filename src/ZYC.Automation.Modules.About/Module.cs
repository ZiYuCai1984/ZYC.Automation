using Autofac;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.About;

internal class Module : ModuleBase
{
    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<AboutTabItemFactory>();
        lifetimeScope.RegisterRootMainMenuItem<AboutMainMenuItem>();

        return Task.CompletedTask;
    }
}