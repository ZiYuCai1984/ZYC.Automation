using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.About;

[RegisterSingleInstance]
internal class AboutMainMenuItem : MainMenuItem
{
    public AboutMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = AboutTabItem.Constants.Title,
            Icon = AboutTabItem.Constants.Icon,
            Anchor = MainMenuAnchors.Update_About
        };

        Command = lifetimeScope.CreateNavigateCommand(AboutTabItem.Constants.Uri);
    }
}