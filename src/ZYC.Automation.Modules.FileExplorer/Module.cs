using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.FileExplorer;

internal class Module : ModuleBase
{
    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<FileExplorerTabItemFactory>();
        lifetimeScope.Resolve<IToolsMainMenuItemsProvider>()
            .RegisterSubItem<FileExplorerMainMenuItem>();

        return Task.CompletedTask;
    }
}