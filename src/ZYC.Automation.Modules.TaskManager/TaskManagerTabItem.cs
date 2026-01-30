using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.Automation.Modules.TaskManager.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager;

[Register]
[ConstantsSource(typeof(TaskManagerModuleConstants))]
internal class TaskManagerTabItem : TabItemInstanceBase<TaskManagerView>
{
    public TaskManagerTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public override void Dispose()
    {
        //ignore
    }
}