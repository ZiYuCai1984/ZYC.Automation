using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update;

[RegisterSingleInstance]
internal class UpdateMainMenuItem : MainMenuItem
{
    public UpdateMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = UpdateTabItem.Constants.Title,
            Icon = UpdateTabItem.Constants.Icon,
            Anchor = MainMenuAnchors.Update_About
        };

        Command = lifetimeScope.CreateNavigateCommand(UpdateTabItem.Constants.Uri);
    }
}