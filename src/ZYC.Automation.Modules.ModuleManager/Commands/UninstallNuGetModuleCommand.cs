using Autofac;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.Commands;

[RegisterSingleInstance]
internal class UninstallNuGetModuleCommand : PairCommandBase<InstallNuGetModuleCommand, UninstallNuGetModuleCommand>
{
    public UninstallNuGetModuleCommand(
        ILifetimeScope lifetimeScope,
        INuGetModuleManager nuGetModuleManager,
        NuGetModuleManifestState nuGetModuleManifestState) : base(lifetimeScope)
    {
        NuGetModuleManager = nuGetModuleManager;
        NuGetModuleManifestState = nuGetModuleManifestState;
    }

    private INuGetModuleManager NuGetModuleManager { get; }

    private NuGetModuleManifestState NuGetModuleManifestState { get; }

    protected override void InternalExecute(object? parameter)
    {
        if (parameter == null)
        {
            return;
        }

        NuGetModuleManager.Uninstall((INuGetModule)parameter);
    }

    public override bool CanExecute(object? parameter)
    {
        if (parameter == null)
        {
            return false;
        }

        var module = (INuGetModule)parameter;

        return !IsExecuting
               && NuGetModuleManifestState.InstalledModules.Any(t =>
                   t.PackageId == module.PackageId && t.Version == module.Version);
    }
}