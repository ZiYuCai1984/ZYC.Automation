using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI;

[RegisterSingleInstance]
internal class CLITabItemFactory : ITabItemFactory
{
    public bool IsSingle => false;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;

        return context.Resolve<CLITabItem>();
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == CLITabItem.Constants.Host)
        {
            return true;
        }

        return false;
    }
}