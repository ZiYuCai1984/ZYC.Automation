using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager;

[RegisterSingleInstance]
internal class TaskManagerTabItemFactory : ITabItemFactory
{
    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;

        return context.Resolve<TaskManagerTabItem>(
            new TypedParameter(typeof(TabReference), new TabReference(context.Uri)));
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == TaskManagerModuleConstants.Host)
        {
            return true;
        }

        return false;
    }
}