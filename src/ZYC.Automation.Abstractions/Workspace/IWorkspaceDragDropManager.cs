namespace ZYC.Automation.Abstractions.Workspace;

public interface IWorkspaceDragDropManager
{
    void Register<T>() where T : IWorkspaceDragDropProvider;
}