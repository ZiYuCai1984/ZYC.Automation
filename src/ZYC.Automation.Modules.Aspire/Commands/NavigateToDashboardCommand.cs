using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.Commands;

[RegisterSingleInstance]
public class NavigateToDashboardCommand : CommandBase, IDisposable
{
    public NavigateToDashboardCommand(
        IEventAggregator eventAggregator,
        ITabManager tabManager,
        IAspireServiceManager aspireServiceManager)
    {
        TabManager = tabManager;
        AspireServiceManager = aspireServiceManager;

        AspireRunningStateChangedEvent =
            eventAggregator.Subscribe<AspireServiceStatusChangedEvent>(
                OnAspireServiceStatusChanged, true);
    }

    private IDisposable AspireRunningStateChangedEvent { get; }

    private ITabManager TabManager { get; }

    private IAspireServiceManager AspireServiceManager { get; }

    public void Dispose()
    {
        AspireRunningStateChangedEvent.Dispose();
    }

    private void OnAspireServiceStatusChanged(AspireServiceStatusChangedEvent obj)
    {
        RaiseCanExecuteChanged();
    }

    public override bool CanExecute(object? parameter)
    {
        var status = AspireServiceManager.GetStatusSnapshot().Status;
        return status == ServiceStatus.Running || status == ServiceStatus.Starting;
    }

    protected override void InternalExecute(object? parameter)
    {
        TabManager.NavigateAsync(AspireTabItem.Constants.Uri);
    }
}