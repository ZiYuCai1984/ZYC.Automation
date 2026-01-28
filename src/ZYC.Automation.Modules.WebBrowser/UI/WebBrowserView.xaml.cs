using Autofac;
using Microsoft.Web.WebView2.Core;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Modules.WebBrowser.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.WebBrowser.UI;

[Register]
internal partial class WebBrowserView
{
    public WebBrowserView(
        IWebBrowserUriPolicy webBrowserUriPolicy,
        ITabManager tabManager,
        ILifetimeScope lifetimeScope,
        Uri uri,
        IWebTabItemInstance instance,
        WebBrowserConfig webBrowserConfig) : base(lifetimeScope)
    {
        WebBrowserUriPolicy = webBrowserUriPolicy;
        TabManager = tabManager;
        Uri = uri;
        Instance = instance;
        WebBrowserConfig = webBrowserConfig;
    }

    private IWebBrowserUriPolicy WebBrowserUriPolicy { get; }
    
    private ITabManager TabManager { get; }

    private Uri Uri { get; }

    private IWebTabItemInstance Instance { get; }

    private WebBrowserConfig WebBrowserConfig { get; }

    protected override bool IsApplyFaviconChanged => true;

    public override string HomePageUri => WebBrowserConfig.StartupPage;

    protected override async Task InternalWebViewHostLoadedAsync()
    {
        await NavigateAsync(Uri);
    }

    protected override void OnDocumentTitleChanged(object? sender, object e)
    {
        base.OnDocumentTitleChanged(sender, e);
        Instance.SetTitle(CoreWebView2.DocumentTitle);
    }

    protected override Task InternalFaviconChangedAsync(object? sender, string base64)
    {
        Instance.SetIcon(base64);
        return Task.CompletedTask;
    }

    protected override async void OnNavigationStarting(
        object? sender,
        CoreWebView2NavigationStartingEventArgs e)
    {
        try
        {
            base.OnNavigationStarting(sender, e);

            var target = e.Uri;
            await Instance.TabInternalNavigatingAsync(new Uri(target));
        }
        catch
        {
            //ignore
        }
    }


    protected override async void OnNewWindowRequested(
        object? sender,
        CoreWebView2NewWindowRequestedEventArgs e)
    {
        try
        {
            //!WARNING There appears to be a bug here.
            e.Handled = true;
            if (!WebBrowserUriPolicy.IsAllowed(new Uri(e.Uri)))
            {
                //TODO-zyc To respond to drag and drop, the event needs to be forwarded to an external location.
                return;
            }

            await TabManager.NavigateAsync(e.Uri);
        }
        catch
        {
            //ignore
        }
    }
}