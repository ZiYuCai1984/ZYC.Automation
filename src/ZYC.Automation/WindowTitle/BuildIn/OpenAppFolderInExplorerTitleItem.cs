using System.Windows.Input;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Abstractions.WindowTitle;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WindowTitle.BuildIn;

[RegisterSingleInstance]
internal class OpenAppFolderInExplorerTitleItem : IWindowTitleItem
{
    public OpenAppFolderInExplorerTitleItem(
        OpenAppFolderInExplorerCommand openAppFolderInExplorerCommand,
        AppConfig appConfig)
    {
        OpenAppFolderInExplorerCommand = openAppFolderInExplorerCommand;
        AppConfig = appConfig;
    }

    private OpenAppFolderInExplorerCommand OpenAppFolderInExplorerCommand { get; }

    private AppConfig AppConfig { get; }

    public string Icon => "FolderOutline";

    public ICommand Command => OpenAppFolderInExplorerCommand;

    public bool IsVisible => true;

    //public bool IsVisible => AppConfig.GetIsDebugItemVisible();
}