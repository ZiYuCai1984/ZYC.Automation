using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Tab.BuildIn;

[Register]
internal class SimpleTabItemFactory : ITabItemFactory
{
    public SimpleTabItemFactory(
        ILifetimeScope lifetimeScope,
        SimpleTabItemFactoryInfo tabItemFactoryInfo,
        IMainMenuManager mainMenuManager)
    {
        LifetimeScope = lifetimeScope;
        TabItemFactoryInfo = tabItemFactoryInfo;

        mainMenuManager.RegisterItem(
            new MainMenuItem(
                MainMenuItemInfo.Title,
                MainMenuItemInfo.Icon,
                lifetimeScope.CreateNavigateCommand(TabItemInfo.Uri),
                MainMenuAnchors.Extension, MainMenuItemInfo.Localization));
    }

    private ILifetimeScope LifetimeScope { get; }

    private SimpleTabItemFactoryInfo TabItemFactoryInfo { get; }

    private SimpleMainMenuItemInfo MainMenuItemInfo => TabItemFactoryInfo.MainMenuItemInfo;

    private SimpleTabItemInfo TabItemInfo => TabItemFactoryInfo.TabItemInfo;

    public bool IsSingle => TabItemFactoryInfo.IsSingle;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return LifetimeScope.Resolve<SimpleTabItem>(
            new TypedParameter(typeof(SimpleTabItemInfo), TabItemInfo));
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        return UriTools.Equals(uri, TabItemInfo.Uri);
    }
}