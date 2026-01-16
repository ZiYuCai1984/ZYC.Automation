using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Commands;

[RegisterSingleInstance]
internal class MergeCommand : AsyncCommandBase<WorkspaceNode>
{
    public MergeCommand(IParallelWorkspaceManager parallelWorkspaceManager)
    {
        ParallelWorkspaceManager = parallelWorkspaceManager;
    }

    private IParallelWorkspaceManager ParallelWorkspaceManager { get; }

    protected override async Task InternalExecuteAsync(WorkspaceNode parameter)
    {
        await ParallelWorkspaceManager.MergeAsync(parameter);
    }

    protected override bool InternalCanExecute(WorkspaceNode? parameter)
    {
        if (parameter == null)
        {
            return false;
        }

        return ParallelWorkspaceManager.CanMerge(parameter);
    }
}