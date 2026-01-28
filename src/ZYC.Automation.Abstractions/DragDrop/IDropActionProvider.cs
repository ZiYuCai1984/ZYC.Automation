namespace ZYC.Automation.Abstractions.DragDrop;

public interface IDropActionProvider
{
    Task<DropAction[]> GetActionsAsync(DropPayload payload, DropContext context);
}