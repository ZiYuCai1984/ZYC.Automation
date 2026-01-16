using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Abstractions.State;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class ExitProcessCommand : CommandBase, IDisposable
{
    public ExitProcessCommand(
        IEventAggregator eventAggregator,
        IAppContext appContext,
        DesktopWindowState desktopWindowState)
    {
        AppContext = appContext;
        DesktopWindowState = desktopWindowState;

        SetPreventExitEvent =
            eventAggregator.Subscribe<SetPreventExitCommandExecutedEvent>(OnSetPreventExitCommandExecuted);
    }

    private IAppContext AppContext { get; }

    private DesktopWindowState DesktopWindowState { get; }

    private IDisposable SetPreventExitEvent { get; }

    public void Dispose()
    {
        SetPreventExitEvent.Dispose();
    }

    private void OnSetPreventExitCommandExecuted(SetPreventExitCommandExecutedEvent obj)
    {
        RaiseCanExecuteChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        return !DesktopWindowState.IsPreventExit;
    }

    protected override void InternalExecute(object? parameter)
    {
        if (!CanExecute(parameter))
        {
            return;
        }

        AppContext.ExitProcess();
    }
}