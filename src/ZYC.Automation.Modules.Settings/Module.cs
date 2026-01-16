using Autofac;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.Settings.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.Settings;

internal class Module : ModuleBase
{
    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<SettingsTabItemFactory>();
        lifetimeScope.RegisterRootMainMenuItem<ISettingsMainMenuItemsProvider>();
        lifetimeScope.RegisterDefaultSideBarItem<SettingsSideBarItem>();

        return Task.CompletedTask;
    }
}