using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.TaskManager.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager;

[Register]
internal class TaskManagerTabItem : TabItemInstanceBase<TaskManagerView>
{
    public TaskManagerTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public override void Dispose()
    {
        //ignore
    }

    public class Constants
    {
        public static string Icon => "DnsOutline";

        public static string Host => "taskmanager";

        public static string Title => "TaskManager";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}