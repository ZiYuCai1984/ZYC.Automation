using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.MainMenu;

[RegisterSingleInstanceAs(typeof(IMainMenuManager))]
internal class MainMenuManager : IMainMenuManager
{
    public MainMenuManager(
        ILifetimeScope lifetimeScope,
        ExitProcessCommand exitProcessCommand,
        RestartCommand restartCommand)
    {
        LifetimeScope = lifetimeScope;

        RegisterItem(new MainMenuItem("Restart",
            "Restart", restartCommand, MainMenuAnchors.Exit));
        RegisterItem(new MainMenuItem("Exit",
            "ExitToApp", exitProcessCommand, MainMenuAnchors.Exit));

        RegisterItem<IToolsMainMenuItemsProvider>();
        RegisterItem<IExtensionsMainMenuItemsProvider>();
    }

    private ILifetimeScope LifetimeScope { get; }

    private IList<IMainMenuItem> Items { get; } = new List<IMainMenuItem>();


    public void RegisterItem(IMainMenuItem item)
    {
        Items.Add(item);
    }

    public void RegisterItem<T>() where T : IMainMenuItem
    {
        RegisterItem(LifetimeScope.Resolve<T>());
    }

    public IMainMenuItem[] GetItems()
    {
        return Items.ToArray();
    }


    public IMainMenuItem?[] GetSortedItems()
    {
        var list = new List<IMainMenuItem?>();

        var groupedItems = GetGroupedMainMenuItems();

        foreach (var group in groupedItems)
        {
            var sortedItems = group.OrderBy(t => t.Priority)
                .Select(SortSubItemsRecursively)
                .ToArray();

            foreach (var item in sortedItems)
            {
                list.Add(item);
            }

            if (group != groupedItems.Last())
            {
                list.Add(null);
            }
        }

        return list.ToArray();
    }

    private IGrouping<string, IMainMenuItem>[] GetGroupedMainMenuItems()
    {
        var items = GetItems();
        var groupedItems =
            items.GroupBy(t => t.Anchor)
                .OrderBy(g => g.Key, StringComparer.Ordinal)
                .ToArray();

        return groupedItems;
    }

    private static IMainMenuItem SortSubItemsRecursively(IMainMenuItem item)
    {
        if (item.SubItems.Length <= 0)
        {
            return item;
        }

        var groupedSubItems = item.SubItems
            .GroupBy(t => t.Anchor)
            .Reverse()
            .SelectMany(group => group.OrderBy(t => t.Priority)
                .Select(SortSubItemsRecursively))
            .ToArray();

        return new MainMenuItemWrapper(item, groupedSubItems);
    }
}