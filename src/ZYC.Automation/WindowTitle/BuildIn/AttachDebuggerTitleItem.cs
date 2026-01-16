using System.Windows.Input;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Abstractions.WindowTitle;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WindowTitle.BuildIn;

[RegisterSingleInstance]
internal class AttachDebuggerTitleItem : IWindowTitleItem
{
    public AttachDebuggerTitleItem(AttachDebuggerCommand attachDebuggerCommand, AppConfig appConfig)
    {
        AttachDebuggerCommand = attachDebuggerCommand;
        AppConfig = appConfig;
    }

    private AttachDebuggerCommand AttachDebuggerCommand { get; }

    private AppConfig AppConfig { get; }

    public string Icon => "BugStopOutline";

    public ICommand Command => AttachDebuggerCommand;

    public bool IsVisible => AppConfig.GetIsDebugItemVisible();
}