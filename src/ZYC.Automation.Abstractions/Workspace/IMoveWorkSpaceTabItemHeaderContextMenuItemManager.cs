using ZYC.Automation.Abstractions.Tab;

namespace ZYC.Automation.Abstractions.Workspace;

public interface IMoveWorkSpaceTabItemHeaderContextMenuItemManager
{
    MoveWorkSpaceTabItemHeaderContextMenuSubItem[] GetSubItems(ITabItemInstance instance);
}