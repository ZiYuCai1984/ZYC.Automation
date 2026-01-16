using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update;

[RegisterSingleInstance]
internal class UpdateTabItemFactory : ITabItemFactory
{
    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return context.Resolve<UpdateTabItem>();
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == UpdateTabItem.Constants.Host)
        {
            return true;
        }

        return false;
    }
}