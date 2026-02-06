using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Build.NewModule.Template.ZYC.Automation.Modules.Chronosynchronicity.Abstractions;
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
            Title = ChronosynchronicityModuleConstants.Title,
            Icon = ChronosynchronicityModuleConstants.Icon,
            Anchor = MainMenuAnchors.Default
        };

        Command = lifetimeScope.CreateNavigateCommand(ChronosynchronicityModuleConstants.Uri);
    }
}