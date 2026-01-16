namespace ZYC.Automation.Abstractions.Event;

public class WorkspaceFocusChangedEvent
{
    public WorkspaceFocusChangedEvent(Guid? id = null)
    {
        Id = id;
    }

    public Guid? Id { get; }
}