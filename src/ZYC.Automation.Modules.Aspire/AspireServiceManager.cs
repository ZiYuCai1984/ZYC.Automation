using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Notification.Toast;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions.Event;
using ZYC.Automation.Modules.Settings.Abstractions.Event;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Abstractions;
using ZYC.CoreToolkit.Dotnet;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire;

[RegisterSingleInstanceAs(typeof(IAspireServiceManager))]
internal sealed partial class AspireServiceManager : IAspireServiceManager, IDisposable
{
    public AspireServiceManager(
        AspireConfig aspireConfig,
        IToastManager toastManager,
        IEventAggregator eventAggregator,
        AspireServiceEnvironment aspireServiceEnvironment,
        ILifetimeScope lifetimeScope,
        IAppLogger<AspireServiceManager> logger)
    {
        AspireConfig = aspireConfig;
        ToastManager = toastManager;
        EventAggregator = eventAggregator;
        AspireServiceEnvironment = aspireServiceEnvironment;
        LifetimeScope = lifetimeScope;
        Logger = logger;

        AspireServiceStartFaultedEvent =
            eventAggregator.Subscribe<AspireServiceStartFaultedAndDisposeFinishedEvent>(
                OnAspireServiceStartFaultedAndDisposeFinished);

        AspireConfigChangedEvent =
            eventAggregator.Subscribe<SettingChangedEvent<AspireConfig>>(OnAspireConfigChanged);
    }

    private IDisposable AspireConfigChangedEvent { get; }

    private SemaphoreSlim Gate { get; } = new(1, 1);

    private AspireConfig AspireConfig { get; }

    private IToastManager ToastManager { get; }

    private IEventAggregator EventAggregator { get; }

    private AspireServiceEnvironment AspireServiceEnvironment { get; }

    private IDisposable AspireServiceStartFaultedEvent { get; }

    private ILifetimeScope LifetimeScope { get; }

    private IAppLogger<AspireServiceManager> Logger { get; }

    private AspireService? AspireService { get; set; }

    public async Task StartServerAsync()
    {
        try
        {
            await Gate.WaitAsync();

            if (AspireService != null)
            {
                await AspireService.StartAsync();
                return;
            }

            AspireService = AspireService.Build(LifetimeScope);
            await AspireService.StartAsync();
        }
        catch (Exception e)
        {
            DebuggerTools.Break();
            Logger.Error(e);
        }
        finally
        {
            Gate.Release();
        }
    }


    public async Task StopServerAsync()
    {
        try
        {
            await Gate.WaitAsync();

            if (AspireService == null)
            {
                throw new InvalidOperationException();
            }

            await AspireService.StopAsync();
            //!WARNING AspireService disposed by self
            AspireService = null;
        }
        catch (Exception e)
        {
            DebuggerTools.Break();
            Logger.Error(e);
        }
        finally
        {
            Gate.Release();
        }
    }

    public AspireServiceStatus GetStatusSnapshot()
    {
        if (AspireService == null)
        {
            return AspireServiceStatus.Stopped();
        }

        return AspireService.GetStatusSnapshot();
    }

    public void SetAspireBinarySource(AspireBinarySource source)
    {
        AspireConfig.AspireBinarySource = source;
        PublishAspireConfigChangedEvent();
    }

    public async Task DownloadAspireToolsAsync()
    {
        var packages = new List<NuGetPackage>
        {
            new()
            {
                Name = AspireServiceEnvironment.OrchestrationPackageName,
                Version = AspireServiceEnvironment.AspirePackageVersion
            },
            new()
            {
                Name = AspireServiceEnvironment.DashboardPackageName,
                Version = AspireServiceEnvironment.AspirePackageVersion
            }
        };

        IOTools.DeleteDirectoryIfExists(AspireServiceEnvironment.AspireToolsFolder);
        await DotnetNuGetTools
            .DownloadNuGetPackagesAsync(packages.ToArray(), AspireServiceEnvironment.AspireToolsFolder)
            .ConfigureAwait(false);
    }

    private void PublishAspireConfigChangedEvent()
    {
        EventAggregator.Publish(new AspireConfigChangedEvent());
    }

    private void OnAspireConfigChanged(SettingChangedEvent<AspireConfig> obj)
    {
        PublishAspireConfigChangedEvent();
    }

    private void OnAspireServiceStartFaultedAndDisposeFinished(AspireServiceStartFaultedAndDisposeFinishedEvent obj)
    {
        //!WARNING AspireService disposed by self
        AspireService = null;
    }
}