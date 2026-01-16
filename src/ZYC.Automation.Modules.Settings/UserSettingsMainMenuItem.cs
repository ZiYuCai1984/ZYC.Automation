using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Settings;

[RegisterSingleInstance]
internal class UserSettingsMainMenuItem : MainMenuItem
{
    public UserSettingsMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Icon = SettingsTabItem.Constants.Icon,
            Title = SettingsTabItem.Constants.Title
        };

        Command = lifetimeScope.CreateNavigateCommand(SettingsTabItem.Constants.Uri);
    }
}