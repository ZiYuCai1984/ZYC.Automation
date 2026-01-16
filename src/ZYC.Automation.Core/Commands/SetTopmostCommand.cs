using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Abstractions.State;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class SetTopmostCommand : CommandBase
{
    public SetTopmostCommand(
        DesktopWindowState desktopWindowState,
        IMainWindow mainWindow,
        IEventAggregator eventAggregator)
    {
        DesktopWindowState = desktopWindowState;
        MainWindow = mainWindow;
        EventAggregator = eventAggregator;
    }

    private DesktopWindowState DesktopWindowState { get; }

    private IMainWindow MainWindow { get; }

    private IEventAggregator EventAggregator { get; }

    protected override void InternalExecute(object? parameter)
    {
        MainWindow.SetTopmost(!DesktopWindowState.Topmost);
        EventAggregator.Publish(new SetTopmostCommandExecutedEvent());
    }
}