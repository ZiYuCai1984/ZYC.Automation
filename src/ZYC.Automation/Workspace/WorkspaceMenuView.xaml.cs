using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Modules.Settings.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Workspace;

[Register]
internal sealed partial class WorkspaceMenuView : IDisposable, INotifyPropertyChanged
{
    private IParallelWorkspaceManager? _parallelWorkspaceManager;

    public WorkspaceMenuView(
        ILifetimeScope lifetimeScope,
        WorkspaceMenuConfig workspaceMenuConfig,
        IEventAggregator eventAggregator,
        WorkspaceNode workspaceNode,
        IWorkspaceMenuManager workspaceMenuManager)
    {
        LifetimeScope = lifetimeScope;
        WorkspaceMenuConfig = workspaceMenuConfig;
        EventAggregator = eventAggregator;
        WorkspaceNode = workspaceNode;
        WorkspaceMenuManager = workspaceMenuManager;

        InitializeComponent();

        WorkspaceMenuConfigChangedEvent =
            EventAggregator.Subscribe<SettingChangedEvent<WorkspaceMenuConfig>>(OnWorkspaceMenuConfigChangedEvent);
    }

    private IDisposable WorkspaceMenuConfigChangedEvent { get; }

    private ILifetimeScope LifetimeScope { get; }

    private WorkspaceMenuConfig WorkspaceMenuConfig { get; }

    public bool IsWorkspaceMenuVisible => WorkspaceMenuConfig.IsVisible;

    private IEventAggregator EventAggregator { get; }

    public WorkspaceNode WorkspaceNode { get; }

    private IWorkspaceMenuManager WorkspaceMenuManager { get; }

    public ObservableCollection<IWorkspaceMenuItem> WorkspaceMenuItems { get; } = new();

    private IParallelWorkspaceManager ParallelWorkspaceManager =>
        _parallelWorkspaceManager ??= LifetimeScope.Resolve<IParallelWorkspaceManager>();

    public void Dispose()
    {
        WorkspaceMenuConfigChangedEvent.Dispose();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnWorkspaceMenuConfigChangedEvent(SettingChangedEvent<WorkspaceMenuConfig> e)
    {
        var oldValue = e.OldValue;
        var newValue = e.NewValue;

        if (oldValue.IsVisible == newValue.IsVisible)
        {
            return;
        }

        OnPropertyChanged(nameof(IsWorkspaceMenuVisible));
    }


    private void OnWorkspaceMenuViewLoaded(object sender, RoutedEventArgs e)
    {
        WorkspaceMenuItems.Clear();
        var items = WorkspaceMenuManager.GetItems().Reverse();

        foreach (var item in items)
        {
            WorkspaceMenuItems.Add(item);
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnDropDownButtonClick(object sender, RoutedEventArgs e)
    {
        ParallelWorkspaceManager.SetFocusedWorkspace(WorkspaceNode);
    }
}