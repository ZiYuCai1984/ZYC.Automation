using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[RegisterSingleInstance]
internal class LocalModuleMainMenuItem : MainMenuItem
{
    public LocalModuleMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = ModuleManagerModuleConstants.Local.Title,
            Icon = ModuleManagerModuleConstants.Local.Icon
        };

        Command = lifetimeScope.CreateNavigateCommand(ModuleManagerModuleConstants.Local.Uri);
    }
}