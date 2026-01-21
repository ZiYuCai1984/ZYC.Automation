using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Notification.Banner;
using ZYC.Automation.Abstractions.Notification.Toast;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.Commands;

[RegisterSingleInstance]
internal class
    UninstallNuGetModuleCommand : AsyncPairCommandBase<InstallNuGetModuleCommand, UninstallNuGetModuleCommand>
{
    public UninstallNuGetModuleCommand(
        IBannerManager bannerManager,
        IToastManager toastManager,
        IAppLogger<InstallNuGetModuleCommand> logger,
        ILifetimeScope lifetimeScope,
        INuGetModuleManager nuGetModuleManager,
        NuGetModuleState nuGetModuleState) : base(lifetimeScope)
    {
        BannerManager = bannerManager;
        ToastManager = toastManager;
        Logger = logger;
        NuGetModuleManager = nuGetModuleManager;
        NuGetModuleState = nuGetModuleState;
    }

    private IBannerManager BannerManager { get; }
    private IToastManager ToastManager { get; }
    private IAppLogger<InstallNuGetModuleCommand> Logger { get; }
    private INuGetModuleManager NuGetModuleManager { get; }

    private NuGetModuleState NuGetModuleState { get; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        if (parameter == null)
        {
            return;
        }

        try
        {
            await NuGetModuleManager.UninstallAsync((INuGetModule)parameter);
            ToastManager.PromptMessage(new ToastMessage("Take effect after restart ."));
            BannerManager.PromptRestart();
        }
        catch (Exception e)
        {
            Logger.Error(e);
            ToastManager.PromptException(e);
        }
    }

    public override bool CanExecute(object? parameter)
    {
        if (parameter == null)
        {
            return false;
        }

        var module = (INuGetModule)parameter;

        return !IsExecuting
               && NuGetModuleState.InstalledModules.Any(t =>
                   t.PackageId == module.PackageId && t.Version == module.Version);
    }
}