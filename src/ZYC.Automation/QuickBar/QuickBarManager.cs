using Autofac;
using ZYC.Automation.Abstractions.QuickBar;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.QuickBar;

[RegisterSingleInstanceAs(typeof(IQuickBarManager))]
internal class QuickBarManager : IQuickBarManager
{
    public QuickBarManager(
        ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private IList<IQuickBarItemsProvider> QuickMenuItemProviders { get; } = new List<IQuickBarItemsProvider>();

    private ILifetimeScope LifetimeScope { get; }

    public void RegisterQuickMenuItemsProvider<T>() where T : IQuickBarItemsProvider
    {
        var provider = LifetimeScope.Resolve<T>();
        QuickMenuItemProviders.Add(provider);
    }

    public void UnregisterQuickMenuItemsProvider<T>() where T : IQuickBarItemsProvider
    {
        throw new NotSupportedException();
    }

    public IQuickBarItem[] GetQuickMenuTitleItems()
    {
        var list = new List<IQuickBarItem>();

        foreach (var provider in QuickMenuItemProviders)
        {
            var items = provider.GetTitleMenuItems();

            if (items.Length != 0)
            {
                list.Add(new QuickBarItemSeparator());
                list.AddRange(items);
            }
        }

        return list.ToArray();
    }
}