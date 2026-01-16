using System.Diagnostics;
using ZYC.CoreToolkit;

namespace ZYC.Automation.Workspace;

internal partial class WorkspaceView : IDisposable
{
    private bool Disposing { get; set; }

    private IDisposable WorkspaceHighlightEvent { get; }

    public void Dispose()
    {
        if (Disposing)
        {
            Debugger.Break();
            return;
        }

        Disposing = true;

        WorkspaceHighlightEvent.Dispose();
        Border.Child?.TryDispose();
    }
}