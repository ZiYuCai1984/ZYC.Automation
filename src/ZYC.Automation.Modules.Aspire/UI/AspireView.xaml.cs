using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.UI;

[Register]
internal partial class AspireView
{
    public AspireView(
        IAspireServiceManager aspireServiceManager,
        IEventAggregator eventAggregator,
        ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
        AspireServiceManager = aspireServiceManager;
        EventAggregator = eventAggregator;
    }

    private IDisposable? AspireDashboardReadyEvent { get; set; }

    private IDisposable? AspireDashboardStartFaultedEvent { get; set; }

    private IAspireServiceManager AspireServiceManager { get; }
    private IEventAggregator EventAggregator { get; }

    private void OnAspireDashboardStartFaulted(AspireServiceStartFaultedAndDisposeFinishedEvent e)
    {
        CoreWebView2.NavigateToString(e.Exception.ToString());
    }

    protected override void InternalDispose()
    {
        base.InternalDispose();

        AspireDashboardReadyEvent?.Dispose();
        AspireDashboardStartFaultedEvent?.Dispose();
    }

    private async void OnAspireDashboardReady(AspireDashboardReadyEvent e)
    {
        await NavigateAsync(e.Uri);
    }

    protected override async Task InternalWebViewHostLoadedAsync()
    {
        var statusSnapshot = AspireServiceManager.GetStatusSnapshot();
        if (statusSnapshot.Status == ServiceStatus.Running)
        {
            await NavigateAsync(statusSnapshot.DashboardUri!);
        }
        else
        {
            AspireDashboardReadyEvent =
                EventAggregator.Subscribe<AspireDashboardReadyEvent>(OnAspireDashboardReady, true);
            AspireDashboardStartFaultedEvent =
                EventAggregator.Subscribe<AspireServiceStartFaultedAndDisposeFinishedEvent>(
                    OnAspireDashboardStartFaulted, true);

            await AspireServiceManager.StartServerAsync();
        }
    }
}