using System.Windows.Input;
using Autofac;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Abstractions.Workspace;

namespace ZYC.Automation.Tab;

internal partial class TabManagerView
{
    private IParallelWorkspaceManager? _parallelWorkspaceManager;

    public bool IsFocusedWorkspace
    {
        get
        {
            var workspace = ParallelWorkspaceManager.GetFocusedWorkspace();
            return workspace == WorkspaceNode;
        }
    }

    public WorkspaceNode WorkspaceNode { get; }


    private IParallelWorkspaceManager ParallelWorkspaceManager =>
        _parallelWorkspaceManager ??= LifetimeScope.Resolve<IParallelWorkspaceManager>();

    private IDisposable WorkspaceFocusChangedEvent { get; }

    private void OnWorkspaceFocusChangedEvent(WorkspaceFocusChangedEvent e)
    {
        OnPropertyChanged(nameof(IsFocusedWorkspace));
    }

    private void OnWorkspaceNodeIndexMouseDown(object sender, MouseButtonEventArgs e)
    {
        ParallelWorkspaceManager.SetFocusedWorkspace(WorkspaceNode);
        e.Handled = false;
    }
}