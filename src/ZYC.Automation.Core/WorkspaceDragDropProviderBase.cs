using System.Windows;
using ZYC.Automation.Abstractions.Workspace;

namespace ZYC.Automation.Core;

public abstract class WorkspaceDragDropProviderBase : IWorkspaceDragDropProvider
{
    public virtual int Priority { get; } = 0;

    public bool HandleDrop(WorkspaceNode node, object dragEventArgs)
    {
        return InternalHandleDrop(node, (DragEventArgs)dragEventArgs);
    }

    public bool HandleDragOver(WorkspaceNode node, object dragEventArgs)
    {
        return InternalHandleDragOver(node, (DragEventArgs)dragEventArgs);
    }

    public bool HandleDragEnter(WorkspaceNode node, object dragEventArgs)
    {
        return InternalHandleDragEnter(node, (DragEventArgs)dragEventArgs);
    }

    protected virtual bool InternalHandleDrop(WorkspaceNode node, DragEventArgs e)
    {
        return false;
    }

    public virtual bool InternalHandleDragOver(WorkspaceNode node, DragEventArgs e)
    {
        return false;
    }

    public virtual bool InternalHandleDragEnter(WorkspaceNode node, DragEventArgs e)
    {
        return false;
    }
}