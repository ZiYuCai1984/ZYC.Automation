using System.Collections.ObjectModel;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Notification.Toast;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.Automation.Modules.ModuleManager.Commands;
using ZYC.Automation.Modules.NuGet.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.UI;

[Register]
internal sealed partial class NuGetModuleManagerView
{
    public NuGetModuleManagerView(
        IToastManager toastManager,
        IAppLogger<NuGetModuleManagerView> logger,
        RefreshNuGetModuleCommand refreshNuGetModuleCommand,
        INuGetModuleManager nuGetModuleManager,
        INuGetManager nuGetManager)
    {
        ToastManager = toastManager;
        Logger = logger;
        RefreshNuGetModuleCommand = refreshNuGetModuleCommand;
        NuGetModuleManager = nuGetModuleManager;
        NuGetManager = nuGetManager;
    }

    private IToastManager ToastManager { get; }
    private IAppLogger<NuGetModuleManagerView> Logger { get; }
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
        try
        {
            var modules = await NuGetModuleManager.GetModulesAsync();
            NuGetModules.Clear();
            foreach (var module in modules)
            {
                NuGetModules.Add(module);
            }

            OnPropertyChanged(nameof(ModulesCount));
        }
        catch (Exception e)
        {
            ToastManager.PromptException(e);
            Logger.Error(e);
        }
    }
}