using System.Windows;

namespace ZYC.Automation.Workspace;

internal partial class WorkspaceView
{
    private void OnWorkspaceViewDragEnter(object sender, DragEventArgs e)
    {
        WorkspaceDragDropManager.OnDragEnter(Node, e);
    }

    private void OnWorkspaceViewDrop(object sender, DragEventArgs e)
    {
        WorkspaceDragDropManager.OnDrop(Node, e);
    }

    private void OnWorkspaceViewDragOver(object sender, DragEventArgs e)
    {
        WorkspaceDragDropManager.OnDragOver(Node, e);
    }
}