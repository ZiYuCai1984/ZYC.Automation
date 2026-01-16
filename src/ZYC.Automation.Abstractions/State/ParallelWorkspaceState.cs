using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.State;

public class ParallelWorkspaceState : IState
{
    public Guid FocusedWorkspaceId { get; set; }
}