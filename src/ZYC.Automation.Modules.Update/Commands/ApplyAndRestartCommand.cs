using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.Commands;

[RegisterSingleInstance]
internal class ApplyAndRestartCommand : AsyncCommandBase
{
    public ApplyAndRestartCommand(
        RestartCommand restartCommand,
        IUpdateManager updateManager)
    {
        RestartCommand = restartCommand;
        UpdateManager = updateManager;
    }

    private RestartCommand RestartCommand { get; }

    private IUpdateManager UpdateManager { get; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        var product = (NewProduct)parameter!;

        await UpdateManager.ApplyProductAsync(product);
        RestartCommand.Execute(null);
    }


    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting;
    }
}