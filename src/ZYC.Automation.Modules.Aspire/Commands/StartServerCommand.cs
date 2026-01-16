using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.Commands;

[RegisterSingleInstance]
internal class StartServerCommand : AsyncCommandBase, IDisposable
{
    public StartServerCommand(
        IAspireServiceManager aspireServiceManager,
        IEventAggregator eventAggregator)
    {
        AspireServiceManager = aspireServiceManager;
        AspireRunningStateChangedEvent =
            eventAggregator.Subscribe<AspireServiceStatusChangedEvent>(
                OnAspireServiceStatusChanged, true);
    }

    private IAspireServiceManager AspireServiceManager { get; }

    private IDisposable AspireRunningStateChangedEvent { get; }

    public void Dispose()
    {
        AspireRunningStateChangedEvent.Dispose();
    }

    private void OnAspireServiceStatusChanged(AspireServiceStatusChangedEvent obj)
    {
        RaiseCanExecuteChanged();
    }

    protected override Task InternalExecuteAsync(object? parameter)
    {
        return AspireServiceManager.StartServerAsync();
    }

    public override bool CanExecute(object? parameter)
    {
        var status = AspireServiceManager.GetStatusSnapshot().Status;
        return status == ServiceStatus.Stopped;
    }
}