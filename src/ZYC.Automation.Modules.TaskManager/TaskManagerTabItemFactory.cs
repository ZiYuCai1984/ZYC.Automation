using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager;

[RegisterSingleInstance]
internal class TaskManagerTabItemFactory : ITabItemFactory
{
    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;

        return context.Resolve<TaskManagerTabItem>();
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == TaskManagerTabItem.Constants.Host)
        {
            return true;
        }

        return false;
    }
}