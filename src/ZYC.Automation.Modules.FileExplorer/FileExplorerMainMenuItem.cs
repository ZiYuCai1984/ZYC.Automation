using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.FileExplorer.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.FileExplorer;

[RegisterSingleInstance]
internal class FileExplorerMainMenuItem : MainMenuItem
{
    public FileExplorerMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Icon = FileExplorerModuleConstants.Icon,
            Title = FileExplorerModuleConstants.MenuTitle
        };

        Command = lifetimeScope.CreateNavigateCommand(FileExplorerModuleConstants.InitialUri);
    }
}