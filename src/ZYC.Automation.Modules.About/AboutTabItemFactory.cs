using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.About;

[RegisterSingleInstance]
internal class AboutTabItemFactory : ITabItemFactory
{
    public bool IsSingle => true;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;

        return context.Resolve<AboutTabItem>();
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == AboutTabItem.Constants.Host)
        {
            return true;
        }

        return false;
    }
}