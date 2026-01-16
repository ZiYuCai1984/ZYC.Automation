using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

// ReSharper disable once CheckNamespace
namespace ZYC.Automation.Modules.Chronosynchronicity;

[RegisterSingleInstance]
internal class ChronosynchronicityTabItemFactory : ITabItemFactory
{
    public bool IsSingle => true;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return context.Resolve<ChronosynchronicityTabItem>();
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == ChronosynchronicityTabItem.Constants.Host)
        {
            return true;
        }

        return false;
    }
}