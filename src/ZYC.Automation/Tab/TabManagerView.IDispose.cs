using System.Diagnostics;
using Autofac;
using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Commands;

namespace ZYC.Automation.Tab;

internal partial class TabManagerView : IDisposable
{
    private IDisposable TabItemsMovedEvent { get; }

    private IDisposable NavigateCompletedEvent { get; }

    private IDisposable TabItemClosedEvent { get; }

    public StarCommand StarCommand => _starCommand ??= LifetimeScope.Resolve<StarCommand>(
        new TypedParameter(typeof(WorkspaceNode), WorkspaceNode),
        new TypedParameter(typeof(TabManagerView), this));


    private bool Disposing { get; set; }

    public void Dispose()
    {
        if (Disposing)
        {
            Debugger.Break();
            return;
        }

        Disposing = true;

        StarCommand.Dispose();

        TabItemsMovedEvent.Dispose();
        NavigateCompletedEvent.Dispose();
        TabItemClosedEvent.Dispose();
        WorkspaceFocusChangedEvent.Dispose();

        TabManagerRestoreCompleted.Dispose();

        DisposeTabManagerViewAsyncFunc.Invoke(WorkspaceNode);
    }
}