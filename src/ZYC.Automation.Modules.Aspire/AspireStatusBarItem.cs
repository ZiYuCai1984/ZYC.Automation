using Autofac;
using ZYC.Automation.Abstractions.StatusBar;
using ZYC.Automation.Modules.Aspire.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire;

[RegisterSingleInstance]
internal class AspireStatusBarItem : IStatusBarItem
{
    public AspireStatusBarItem(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    public object View => LifetimeScope.Resolve<AspireStatusBarItemView>();

    public StatusBarSection Section => StatusBarSection.Right;
}