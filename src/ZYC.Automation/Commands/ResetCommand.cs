using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Commands;

[RegisterSingleInstance]
internal class ResetCommand : AsyncCommandBase<WorkspaceNode>
{
    public ResetCommand(IParallelWorkspaceManager parallelWorkspaceManager)
    {
        ParallelWorkspaceManager = parallelWorkspaceManager;
    }

    private IParallelWorkspaceManager ParallelWorkspaceManager { get; }

    protected override async Task InternalExecuteAsync(WorkspaceNode parameter)
    {
        await ParallelWorkspaceManager.ResetAsync();
    }
}