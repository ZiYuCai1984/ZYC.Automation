using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.BlazorDemo;

[RegisterSingleInstance]
internal class BlazorDemoMainMenuItem : MainMenuItem
{
    public BlazorDemoMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = BlazorDemoTabItem.Constants.Title,
            Icon = BlazorDemoTabItem.Constants.Icon,
            Anchor = MainMenuAnchors.Default,
            Localization = false
        };

        Command = lifetimeScope.CreateNavigateCommand(BlazorDemoTabItem.Constants.Uri);
    }
}