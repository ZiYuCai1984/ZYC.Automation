using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[RegisterSingleInstance]
internal class LocalModuleMainMenuItem : MainMenuItem
{
    public LocalModuleMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = LocalModuleTabItem.Constants.Title,
            Icon = LocalModuleTabItem.Constants.Icon
        };

        Command = lifetimeScope.CreateNavigateCommand(LocalModuleTabItem.Constants.Uri);
    }
}