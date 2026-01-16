using System.Collections.ObjectModel;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.Automation.Modules.NuGet.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.UI;

[Register]
internal sealed partial class NuGetModuleManagerView
{
    public NuGetModuleManagerView(INuGetModuleManager nuGetModuleManager, INuGetManager nuGetManager)
    {
        NuGetModuleManager = nuGetModuleManager;
        NuGetManager = nuGetManager;
    }

    private INuGetModuleManager NuGetModuleManager { get; }

    private INuGetManager NuGetManager { get; }

    public ObservableCollection<INuGetModule> NuGetModules { get; } = new();
}