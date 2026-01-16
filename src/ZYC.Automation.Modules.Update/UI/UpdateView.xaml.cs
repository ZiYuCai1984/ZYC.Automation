using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.Automation.Modules.Update.Abstractions.Event;
using ZYC.Automation.Modules.Update.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.UI;

[Register]
internal sealed partial class UpdateView
{
    public UpdateView(
        IEventAggregator eventAggregator,
        ClearNuGetHttpCacheCommand clearNuGetHttpCacheCommand,
        IUpdateManager updateManager,
        DownloadCommand downloadCommand,
        CheckUpdateCommand checkUpdateCommand,
        ILifetimeScope lifetimeScope,
        ApplyAndRestartCommand applyAndRestartCommand)
    {
        EventAggregator = eventAggregator;
        ClearNuGetHttpCacheCommand = clearNuGetHttpCacheCommand;
        UpdateManager = updateManager;

        DownloadCommand = downloadCommand;
        CheckUpdateCommand = checkUpdateCommand;
        ApplyAndRestartCommand = applyAndRestartCommand;

        LifetimeScope = lifetimeScope;

        UpdateContextChangedEvent = EventAggregator.Subscribe<UpdateContextChangedEvent>(OnUpdateContextChangedEvent);
    }


    private IDisposable UpdateContextChangedEvent { get; }

    public CheckUpdateCommand CheckUpdateCommand { get; }

    public ApplyAndRestartCommand ApplyAndRestartCommand { get; }

    /// <summary>
    ///     !WARNING Used for binding, cannot be set to private
    /// </summary>
    public ILifetimeScope LifetimeScope { get; }

    private IEventAggregator EventAggregator { get; }

    public ClearNuGetHttpCacheCommand ClearNuGetHttpCacheCommand { get; }

    private IUpdateManager UpdateManager { get; }

    public DownloadCommand DownloadCommand { get; }

    public UpdateStatus UpdateStatus => UpdateContext.UpdateStatus;

    public Exception? UpdateException => UpdateContext.Exception;

    public IProduct? NewProduct => UpdateContext.NewProduct;

    private UpdateContext UpdateContext => UpdateManager.GetCurrentUpdateContext();


    private void OnUpdateContextChangedEvent(UpdateContextChangedEvent e)
    {
        OnPropertyChanged(nameof(UpdateException));
        OnPropertyChanged(nameof(NewProduct));
        OnPropertyChanged(nameof(UpdateStatus));
    }


    public override void Dispose()
    {
        base.Dispose();

        UpdateContextChangedEvent.Dispose();
    }

    protected override void InternalOnLoaded()
    {
        //!WARNING UpdateStatus.Free -> Start check update
        if (UpdateStatus == UpdateStatus.Free)
        {
            CheckUpdateCommand.Execute(null);
        }
    }
}