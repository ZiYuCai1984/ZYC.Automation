namespace ZYC.Automation.Abstractions.SideBar;

public interface ISideBarManager
{
    void RegisterSideBarItemsProvider<T>() where T : ISideBarItemsProvider;

    void UnregisterSideBarItemsProvider<T>() where T : ISideBarItemsProvider;

    ISideBarItem[] GetAllItems();

    ISideBarItem[] GetTopItems()
    {
        return GetAllItems().Where(t => t.Section == SideBarSection.Top).ToArray();
    }

    ISideBarItem[] GetBottomItems()
    {
        return GetAllItems().Where(t => t.Section == SideBarSection.Bottom).ToArray();
    }
}