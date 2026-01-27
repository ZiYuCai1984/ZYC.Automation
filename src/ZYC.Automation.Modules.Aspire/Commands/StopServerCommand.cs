using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.Commands;

[RegisterSingleInstance]
internal class StopServerCommand : AsyncCommandBase, IDisposable
{
    public StopServerCommand(
        IAppContext appContext,
        IAspireServiceManager aspireServiceManager,
        IEventAggregator eventAggregator)
    {
        AppContext = appContext;
        AspireServiceManager = aspireServiceManager;

        eventAggregator.Subscribe<AspireServiceStatusChangedEvent>(OnAspireServiceStatusChanged, true)
            .DisposeWith(CompositeDisposable);
    }

    private CompositeDisposable CompositeDisposable { get; } = new();

    private IAppContext AppContext { get; }

    private IAspireServiceManager AspireServiceManager { get; }

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }

    private void OnAspireServiceStatusChanged(AspireServiceStatusChangedEvent e)
    {
        RaiseCanExecuteChanged();
    }

    protected override Task InternalExecuteAsync(object? parameter)
    {
        return AspireServiceManager.StopServerAsync();
    }

    public override bool CanExecute(object? parameter)
    {
        var status = AspireServiceManager.GetStatusSnapshot().Status;
        return status == ServiceStatus.Running;
    }
}