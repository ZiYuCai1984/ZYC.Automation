namespace ZYC.Automation.Abstractions.DragDrop;

public sealed record DropResolution(
    DropAction[] Actions,
    DropResolutionMode Mode,
    DropAction? DefaultAction);