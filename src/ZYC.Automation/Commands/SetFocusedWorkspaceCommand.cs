using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Commands;

[RegisterSingleInstance]
internal class SetFocusedWorkspaceCommand : CommandBase<WorkspaceNode>
{
    public SetFocusedWorkspaceCommand(
        IParallelWorkspaceManager parallelWorkspaceManager)
    {
        ParallelWorkspaceManager = parallelWorkspaceManager;
    }

    private IParallelWorkspaceManager ParallelWorkspaceManager { get; }

    protected override void InternalExecute(WorkspaceNode parameter)
    {
        ParallelWorkspaceManager.SetFocusedWorkspace(parameter);
    }

    protected override bool InternalCanExecute(WorkspaceNode parameter)
    {
        return ParallelWorkspaceManager.GetFocusedWorkspace() != parameter;
    }
}