using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.WebBrowser.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.WebBrowser;

internal class Module : ModuleBase
{
    public override string Icon => WebBrowserModuleConstants.MenuIcon;

    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<WebBrowserTabItemFactory>();
        lifetimeScope.Resolve<IToolsMainMenuItemsProvider>()
            .RegisterSubItem<WebBrowserMainMenuItem>();

        return Task.CompletedTask;
    }
}