namespace ZYC.Automation.Abstractions.DragDrop;

public interface IDropHandler
{
    int Order { get; }
    
    bool CanHandle(DropPayload payload, DropContext context);

    Task HandleAsync(DropPayload payload, DropContext context);
}