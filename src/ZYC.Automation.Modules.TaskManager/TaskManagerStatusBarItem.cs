using Autofac;
using ZYC.Automation.Abstractions.StatusBar;
using ZYC.Automation.Modules.TaskManager.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager;

[RegisterSingleInstance]
internal class TaskManagerStatusBarItem : IStatusBarItem
{
    public TaskManagerStatusBarItem(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    public object View => LifetimeScope.Resolve<TaskManagerStatusBarItemView>();

    public StatusBarSection Section => StatusBarSection.Right;
}