using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;


namespace ZYC.Automation.Modules.MarkdownViewer;

[RegisterSingleInstance]
internal class MarkdownViewerMainMenuItem : MainMenuItem
{
    public MarkdownViewerMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = MarkdownViewerTabItem.Constants.Title,
            Icon = MarkdownViewerTabItem.Constants.Icon,
            Anchor = MainMenuAnchors.Default
        };

        Command = lifetimeScope.CreateNavigateCommand(MarkdownViewerTabItem.Constants.Uri);
    }
}