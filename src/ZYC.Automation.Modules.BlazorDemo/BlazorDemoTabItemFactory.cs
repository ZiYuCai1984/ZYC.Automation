using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.BlazorDemo;

[RegisterSingleInstance]
internal class BlazorDemoTabItemFactory : ITabItemFactory
{
    public bool IsSingle => true;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;

        return context.Resolve<BlazorDemoTabItem>();
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == BlazorDemoTabItem.Constants.Host)
        {
            return true;
        }

        return false;
    }
}