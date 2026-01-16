namespace ZYC.Automation.Abstractions.SideBar;

public interface ISideBarItemsProvider
{
    ISideBarItem[] GetSideBarItems();

    void RegisterItem<T>() where T : ISideBarItem;

    void RegisterItem(ISideBarItem item);

    void UnregisterItem<T>() where T : ISideBarItem;

    void UnregisterItem(ISideBarItem item);
}