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
        ITabManager tabManager,
        ILifetimeScope lifetimeScope,
        Uri uri,
        IWebTabItemInstance instance,
        WebBrowserConfig webBrowserConfig) : base(lifetimeScope)
    {
        TabManager = tabManager;
        Uri = uri;
        Instance = instance;
        WebBrowserConfig = webBrowserConfig;
    }

    private ITabManager TabManager { get; }

    private Uri Uri { get; }

    private IWebTabItemInstance Instance { get; }

    private WebBrowserConfig WebBrowserConfig { get; }

    protected override bool IsApplyFaviconChanged => true;

    public override string? HomePageUri => WebBrowserConfig.StartupPage;

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
        base.OnNavigationStarting(sender, e);

        var target = e.Uri;
        await Instance.TabInternalNavigatingAsync(new Uri(target));
    }


    protected override async void OnNewWindowRequested(
        object? sender,
        CoreWebView2NewWindowRequestedEventArgs e)
    {
        e.Handled = true;
        await TabManager.NavigateAsync(e.Uri);
    }
}