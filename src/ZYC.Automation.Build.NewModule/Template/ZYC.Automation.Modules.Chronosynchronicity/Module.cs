using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac;

// ReSharper disable once CheckNamespace
namespace ZYC.Automation.Modules.Chronosynchronicity;

internal class Module : ModuleBase
{
    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<ChronosynchronicityTabItemFactory>();
        lifetimeScope.RegisterRootMainMenuItem<ChronosynchronicityMainMenuItem>();
        lifetimeScope.Resolve<IExtensionsMainMenuItemsProvider>()
            .RegisterSubItem<ChronosynchronicityMainMenuItem>();

        return Task.CompletedTask;
    }
}