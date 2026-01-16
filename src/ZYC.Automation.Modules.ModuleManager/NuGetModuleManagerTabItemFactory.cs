using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[RegisterSingleInstance]
internal class NuGetModuleManagerTabItemFactory : ITabItemFactory
{
    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return context.Resolve<NuGetModuleTabItem>();
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (UriTools.IsPathMatched(uri, NuGetModuleTabItem.Constants.Path))
        {
            return true;
        }

        return false;
    }
}