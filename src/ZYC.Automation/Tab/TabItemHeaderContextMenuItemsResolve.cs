using System.Windows.Markup;
using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Resources;

namespace ZYC.Automation.Tab;

internal class TabItemHeaderContextMenuItemsResolve : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var lifetimeScope = LifetimeScopeResource.GetRootLifetimeScopeFromMainWindowDataContext();

        var items = lifetimeScope.Resolve<ITabItemHeaderContextMenuItemView[]>();

        return items.OrderBy(t => t.Order);
    }
}