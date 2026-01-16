using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI;

[RegisterSingleInstance]
internal class CLIMainMenuItem : MainMenuItem
{
    public CLIMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = CLITabItem.Constants.Title,
            Icon = CLITabItem.Constants.Icon,
            Anchor = MainMenuAnchors.Default,
            Localization = false
        };

        Command = lifetimeScope.CreateNavigateCommand(CLITabItem.Constants.Uri);
    }
}