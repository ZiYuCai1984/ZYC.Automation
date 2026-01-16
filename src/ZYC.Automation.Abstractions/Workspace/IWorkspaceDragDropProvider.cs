namespace ZYC.Automation.Abstractions.Workspace;

public interface IWorkspaceDragDropProvider
{
    int Priority { get; }

    bool HandleDrop(WorkspaceNode node, object dragEventArgs);

    bool HandleDragOver(WorkspaceNode node, object dragEventArgs);

    bool HandleDragEnter(WorkspaceNode node, object dragEventArgs);
}