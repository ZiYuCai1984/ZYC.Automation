using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.FileExplorer;

[RegisterSingleInstance]
internal class FileExplorerMainMenuItem : MainMenuItem
{
    public FileExplorerMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Icon = FileExplorerTabItem.Constants.Icon,
            Title = FileExplorerTabItem.Constants.MenuTitle
        };

        Command = lifetimeScope.CreateNavigateCommand(FileExplorerTabItem.Constants.DefaultUri);
    }
}