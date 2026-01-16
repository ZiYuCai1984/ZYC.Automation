using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.WebBrowser;

[RegisterSingleInstance]
internal class WebBrowserTabItemFactory : ITabItemFactory
{
    public bool IsSingle => false;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return context.Resolve<WebBrowserTabItem>(
            new TypedParameter(typeof(Uri), context.Uri));
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Scheme == "http"
            || uri.Scheme == "https"
            || uri.Scheme == "chrome-extension"
            || uri.Scheme == "extension")
        {
            return true;
        }

        return false;
    }
}