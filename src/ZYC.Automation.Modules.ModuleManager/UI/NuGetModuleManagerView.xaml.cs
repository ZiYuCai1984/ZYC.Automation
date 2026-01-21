using System.Collections.ObjectModel;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.Automation.Modules.ModuleManager.Commands;
using ZYC.Automation.Modules.NuGet.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.UI;

[Register]
internal sealed partial class NuGetModuleManagerView
{
    public NuGetModuleManagerView(
        RefreshNuGetModuleCommand refreshNuGetModuleCommand,
        INuGetModuleManager nuGetModuleManager,
        INuGetManager nuGetManager)
    {
        RefreshNuGetModuleCommand = refreshNuGetModuleCommand;
        NuGetModuleManager = nuGetModuleManager;
        NuGetManager = nuGetManager;
    }

    private RefreshNuGetModuleCommand RefreshNuGetModuleCommand { get; }

    private INuGetModuleManager NuGetModuleManager { get; }

    private INuGetManager NuGetManager { get; }

    public ObservableCollection<INuGetModule> NuGetModules { get; } = new();

    public int ModulesCount => NuGetModules.Count;

    protected override async void InternalOnLoaded()
    {
        base.InternalOnLoaded();
        await RefreshNuGetModuleCommand.ExecuteAsync(this);
    }

    public async Task RefreshNuGetModulesAsync()
    {
        var modules = await NuGetModuleManager.GetModulesAsync();
        NuGetModules.Clear();
        foreach (var module in modules)
        {
            NuGetModules.Add(module);
        }

        OnPropertyChanged(nameof(ModulesCount));
    }
}