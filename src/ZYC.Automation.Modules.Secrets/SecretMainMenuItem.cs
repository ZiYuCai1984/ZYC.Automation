using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Secrets;

[RegisterSingleInstance]
internal class SecretMainMenuItem : MainMenuItem
{
    public SecretMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = SecretsTabItem.Constants.Title,
            Icon = SecretsTabItem.Constants.Icon
        };

        Command = lifetimeScope.CreateNavigateCommand(SecretsTabItem.Constants.Uri);
    }
}