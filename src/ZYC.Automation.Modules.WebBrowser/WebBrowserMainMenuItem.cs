using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.WebBrowser.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.WebBrowser;

[RegisterSingleInstance]
public class WebBrowserMainMenuItem : MainMenuItem
{
    public WebBrowserMainMenuItem(
        ILifetimeScope lifetimeScope,
        WebBrowserConfig webBrowserConfig)
    {
        Command = lifetimeScope.CreateNavigateCommand(new Uri(webBrowserConfig.StartupUri));
        Info = new MenuItemInfo
        {
            Title = WebBrowserModuleConstants.MenuTitle,
            Icon = WebBrowserModuleConstants.MenuIcon
        };
    }
}