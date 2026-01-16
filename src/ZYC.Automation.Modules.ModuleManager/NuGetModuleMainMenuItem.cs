using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[RegisterSingleInstance]
internal class NuGetModuleMainMenuItem : MainMenuItem
{
    public NuGetModuleMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = NuGetModuleTabItem.Constants.Title,
            Icon = NuGetModuleTabItem.Constants.Icon
        };

        Command = lifetimeScope.CreateNavigateCommand(NuGetModuleTabItem.Constants.Uri);
    }
}