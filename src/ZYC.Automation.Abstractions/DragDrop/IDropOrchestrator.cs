namespace ZYC.Automation.Abstractions.DragDrop;

public interface IDropOrchestrator
{
    Task<DropResolution> ResolveAsync(DropPayload payload, DropContext context);
}