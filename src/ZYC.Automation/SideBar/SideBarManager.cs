using Autofac;
using ZYC.Automation.Abstractions.SideBar;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.SideBar;

[RegisterSingleInstanceAs(typeof(ISideBarManager))]
internal class SideBarManager : ISideBarManager
{
    public SideBarManager(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;

        RegisterSideBarItemsProvider<IDefaultSideBarItemsProvider>();
    }

    private ILifetimeScope LifetimeScope { get; }


    private IList<ISideBarItemsProvider> SideBarItemsProviders { get; } = new List<ISideBarItemsProvider>();

    public void RegisterSideBarItemsProvider<T>() where T : ISideBarItemsProvider
    {
        var provider = LifetimeScope.Resolve<T>();
        SideBarItemsProviders.Add(provider);
    }

    public void UnregisterSideBarItemsProvider<T>() where T : ISideBarItemsProvider
    {
        throw new NotImplementedException();
    }

    public ISideBarItem[] GetAllItems()
    {
        var sideBarItems = new List<ISideBarItem>();

        var providers = SideBarItemsProviders.ToArray();
        foreach (var provider in providers)
        {
            sideBarItems.AddRange(provider.GetSideBarItems());
        }

        return sideBarItems.OrderBy(t => t.Order).ToArray();
    }
}