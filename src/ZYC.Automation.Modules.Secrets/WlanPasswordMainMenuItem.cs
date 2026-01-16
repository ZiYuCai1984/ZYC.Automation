using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Secrets;

[RegisterSingleInstance]
internal class WlanPasswordMainMenuItem : MainMenuItem
{
    public WlanPasswordMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = WlanPasswordTabItem.Constants.Title,
            Icon = WlanPasswordTabItem.Constants.Icon
        };

        Command = lifetimeScope.CreateNavigateCommand(WlanPasswordTabItem.Constants.Uri);
    }
}