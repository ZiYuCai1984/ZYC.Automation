using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Abstractions.State;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class SetPreventExitCommand : CommandBase
{
    public SetPreventExitCommand(
        DesktopWindowState desktopWindowState,
        IEventAggregator eventAggregator)
    {
        DesktopWindowState = desktopWindowState;
        EventAggregator = eventAggregator;
    }

    private DesktopWindowState DesktopWindowState { get; }

    private IEventAggregator EventAggregator { get; }

    protected override void InternalExecute(object? parameter)
    {
        DesktopWindowState.IsPreventExit = !DesktopWindowState.IsPreventExit;
        EventAggregator.Publish(new SetPreventExitCommandExecutedEvent());
    }
}