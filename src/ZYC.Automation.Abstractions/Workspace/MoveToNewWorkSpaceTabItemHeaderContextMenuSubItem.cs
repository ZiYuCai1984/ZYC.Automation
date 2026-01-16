using System.Windows.Input;

namespace ZYC.Automation.Abstractions.Workspace;

public class MoveToNewWorkSpaceTabItemHeaderContextMenuSubItem : MoveWorkSpaceTabItemHeaderContextMenuSubItem
{
    public MoveToNewWorkSpaceTabItemHeaderContextMenuSubItem(string title, ICommand command) : base(null!, title,
        command)
    {
    }
}