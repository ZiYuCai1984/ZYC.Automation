using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class FullScreenCommand : CommandBase
{
    public FullScreenCommand(IMainWindow mainWindow, IEventAggregator eventAggregator)
    {
        MainWindow = mainWindow;
        EventAggregator = eventAggregator;
    }

    private IMainWindow MainWindow { get; }

    private IEventAggregator EventAggregator { get; }

    protected override void InternalExecute(object? parameter)
    {
        var state = MainWindow.GetWindowState();

        if (state == WindowState.Maximized)
        {
            MainWindow.SetWindowState(WindowState.Normal);
        }
        else if (state == WindowState.Normal)
        {
            MainWindow.SetWindowState(WindowState.Maximized);
        }

        EventAggregator.Publish(new FullScreennCommandExecutedEvent());
    }
}