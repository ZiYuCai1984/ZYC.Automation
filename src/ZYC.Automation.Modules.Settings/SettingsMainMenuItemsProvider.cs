using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core.Menu;
using ZYC.Automation.Modules.Settings.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Settings;

[RegisterSingleInstanceAs(typeof(ISettingsMainMenuItemsProvider))]
internal class SettingsMainMenuItemsProvider : MainMenuItemsProvider, ISettingsMainMenuItemsProvider
{
    public SettingsMainMenuItemsProvider(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Anchor = MainMenuAnchors.Setting_Manager,
            Icon = "CogOutline",
            Title = "Settings"
        };

        RegisterSubItem<UserSettingsMainMenuItem>();
        RegisterSubItem<ResetAllMainMenuItem>();
    }

    public override MenuItemInfo Info { get; }
}