using System.Diagnostics;
using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class CloseTabItemCommand : CommandBase
{
    public CloseTabItemCommand(ITabManager tabManager)
    {
        TabManager = tabManager;
    }

    private ITabManager TabManager { get; }

    public override bool CanExecute(object? parameter)
    {
        return parameter != null;
    }

    protected override void InternalExecute(object? parameter)
    {
        if (parameter == null)
        {
            Debugger.Break();
        }

        var instance = (ITabItemInstance)parameter!;
        TabManager.CloseAsync(instance);
    }
}