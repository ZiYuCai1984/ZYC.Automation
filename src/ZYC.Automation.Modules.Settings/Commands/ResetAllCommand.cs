using Autofac;
using ZYC.Automation.Core;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.Settings.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Settings.Commands;

[RegisterSingleInstance]
internal class ResetAllCommand : AsyncCommandBase
{
    public ResetAllCommand(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        await base.InternalExecuteAsync(parameter);

        if (!MessageBoxTools.Confirm("WARNING: This operation is irreversible !!"))
        {
            return;
        }

        var settingsManager = LifetimeScope.Resolve<ISettingsManager>();
        await settingsManager.ResetAllAsync();


        var restartCommand = LifetimeScope.Resolve<RestartCommand>();
        restartCommand.Execute(null);
    }
}