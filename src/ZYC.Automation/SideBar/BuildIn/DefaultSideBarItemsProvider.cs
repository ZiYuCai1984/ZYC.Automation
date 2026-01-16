using Autofac;
using ZYC.Automation.Abstractions.SideBar;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.SideBar.BuildIn;

[RegisterSingleInstanceAs(typeof(IDefaultSideBarItemsProvider))]
internal class DefaultSideBarItemsProvider : IDefaultSideBarItemsProvider
{
    public DefaultSideBarItemsProvider(
        ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    private IList<ISideBarItem> SideBarItems { get; } = new List<ISideBarItem>();

    public ISideBarItem[] GetSideBarItems()
    {
        return SideBarItems.ToArray();
    }

    public void RegisterItem<T>() where T : ISideBarItem
    {
        SideBarItems.Add(LifetimeScope.Resolve<T>());
    }

    public void RegisterItem(ISideBarItem item)
    {
        SideBarItems.Add(item);
    }

    public void UnregisterItem<T>() where T : ISideBarItem
    {
        throw new NotImplementedException();
    }

    public void UnregisterItem(ISideBarItem item)
    {
        throw new NotImplementedException();
    }
}