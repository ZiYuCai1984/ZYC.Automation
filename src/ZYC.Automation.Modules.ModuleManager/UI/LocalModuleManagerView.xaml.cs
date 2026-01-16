using System.Collections.ObjectModel;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.Automation.Modules.ModuleManager.Abstractions.Event;
using ZYC.CoreToolkit.Abstractions.Autofac;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.UI;

[Register]
public partial class LocalModuleManagerView
{
    public LocalModuleManagerView(
        IEventAggregator eventAggregator,
        ILocalModuleManager localModuleManager,
        ModuleConfig moduleConfig)
    {
        EventAggregator = eventAggregator;
        LocalModuleManager = localModuleManager;
        ModuleConfig = moduleConfig;

        DeleteModuleEvent = EventAggregator.Subscribe<DeleteModuleEvent>(_ =>
        {
            OnPropertyChanged(nameof(ModulePendingDeleteAssemblyNames));
        });

        DisableModuleEvent = EventAggregator.Subscribe<DisableModuleEvent>(_ =>
        {
            OnPropertyChanged(nameof(ModuleDisabledAssemblyNames));
        });

        EnableModuleEvent = EventAggregator.Subscribe<EnableModuleEvent>(_ =>
        {
            OnPropertyChanged(nameof(ModuleDisabledAssemblyNames));
        });


        InitializeComponent();
    }

    private IDisposable DisableModuleEvent { get; }

    private IDisposable EnableModuleEvent { get; }

    private IDisposable DeleteModuleEvent { get; }

    private IEventAggregator EventAggregator { get; }

    private ILocalModuleManager LocalModuleManager { get; }

    private ModuleConfig ModuleConfig { get; }

    public string[] ModuleDisabledAssemblyNames => ModuleConfig.DisabledAssemblyNames;

    public string[] ModulePendingDeleteAssemblyNames => LocalModuleManager.GetPendingRemoveDlls();

    public ObservableCollection<IModuleInfo> ModuleInfos { get; } = new();

    protected override void InternalOnLoaded()
    {
        var modules = LocalModuleManager.GetModules();
        foreach (var module in modules)
        {
            ModuleInfos.Add(module);
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        DeleteModuleEvent.Dispose();
        DisableModuleEvent.Dispose();
        EnableModuleEvent.Dispose();
    }
}