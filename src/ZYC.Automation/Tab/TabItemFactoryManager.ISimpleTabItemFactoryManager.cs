using Autofac;
using ZYC.Automation.Abstractions.QuickBar;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.Automation.Tab.BuildIn;

namespace ZYC.Automation.Tab;

internal partial class TabItemFactoryManager : ISimpleTabItemFactoryManager
{
    public void Register(SimpleTabItemFactoryInfo info)
    {
        var factory = LifetimeScope.Resolve<SimpleTabItemFactory>(
            new TypedParameter(typeof(SimpleTabItemFactoryInfo), info));
        TabItemFactories.Add(factory);


        if (info.AddQuickBarItem)
        {
            var navigateCommand = LifetimeScope.CreateNavigateCommand(info.TabItemInfo.Uri);

            var simpleQuickBarItemsProvider = LifetimeScope.Resolve<ISimpleQuickBarItemsProvider>();
            simpleQuickBarItemsProvider.AttachItem(
                new SimpleQuickBarItem(
                    info.TabItemInfo.Uri, info.TabItemInfo.Icon,
                    navigateCommand, info.TabItemInfo.Title));
        }
    }
}