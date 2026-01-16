using ZYC.Automation.Abstractions.Tab;

namespace ZYC.Automation.Abstractions.Event;

public class NavigateCompletedEvent
{
    public NavigateCompletedEvent(
        Guid workspaceId,
        ITabItemInstance tabItemInstance)
    {
        WorkspaceId = workspaceId;
        TabItemInstance = tabItemInstance;
    }

    public Guid WorkspaceId { get; }

    public ITabItemInstance TabItemInstance { get; }
}