using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

// ReSharper disable once CheckNamespace
namespace ZYC.Automation.Modules.Chronosynchronicity;

[RegisterSingleInstance]
internal class ChronosynchronicityMainMenuItem : MainMenuItem
{
    public ChronosynchronicityMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = ChronosynchronicityTabItem.Constants.Title,
            Icon = ChronosynchronicityTabItem.Constants.Icon,
            Anchor = MainMenuAnchors.Default
        };

        Command = lifetimeScope.CreateNavigateCommand(ChronosynchronicityTabItem.Constants.Uri);
    }
}