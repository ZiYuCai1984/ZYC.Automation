using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Commands;

[RegisterSingleInstance]
internal class SplitVerticalCommand : AsyncCommandBase<WorkspaceNode>
{
    public SplitVerticalCommand(IParallelWorkspaceManager parallelWorkspaceManager)
    {
        ParallelWorkspaceManager = parallelWorkspaceManager;
    }

    private IParallelWorkspaceManager ParallelWorkspaceManager { get; }

    protected override async Task InternalExecuteAsync(WorkspaceNode parameter)
    {
        await ParallelWorkspaceManager.SplitVerticalAsync(parameter);
    }
}