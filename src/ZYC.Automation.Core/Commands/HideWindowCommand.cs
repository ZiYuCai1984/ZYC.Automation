using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class HideWindowCommand : CommandBase
{
    public HideWindowCommand(IMainWindow mainWindow)
    {
        MainWindow = mainWindow;
    }

    private IMainWindow MainWindow { get; }

    protected override void InternalExecute(object? parameter)
    {
        MainWindow.Hide();
    }
}