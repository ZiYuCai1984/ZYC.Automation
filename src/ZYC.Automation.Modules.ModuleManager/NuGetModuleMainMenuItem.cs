using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[RegisterSingleInstance]
internal class NuGetModuleMainMenuItem : MainMenuItem
{
    public NuGetModuleMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = ModuleManagerModuleConstants.NuGet.Title,
            Icon = ModuleManagerModuleConstants.NuGet.Icon
        };

        Command = lifetimeScope.CreateNavigateCommand(ModuleManagerModuleConstants.NuGet.Uri);
    }
}