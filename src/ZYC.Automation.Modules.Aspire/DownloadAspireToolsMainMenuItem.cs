using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Modules.Aspire.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire;

[RegisterSingleInstance]
internal class DownloadAspireToolsMainMenuItem : MainMenuItem
{
    public DownloadAspireToolsMainMenuItem(DownloadAspireToolsCommand downloadAspireToolsCommand)
    {
        Info = new MenuItemInfo
        {
            Title = "Download aspire tools",
            Icon = "MonitorArrowDown"
        };

        Command = downloadAspireToolsCommand;
    }
}