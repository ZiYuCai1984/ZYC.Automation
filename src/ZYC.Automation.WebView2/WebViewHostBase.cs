using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using Microsoft.Web.WebView2.Core;
using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.WebView2;

namespace ZYC.Automation.WebView2;

// ReSharper disable VirtualMemberNeverOverridden.Global
// ReSharper disable UnusedMember.Global
public abstract partial class WebViewHostBase : UserControl, IDisposable
{
    protected WebViewHostBase(
        ILifetimeScope lifetimeScope)
    {
        ComponentTools.TryCallInitializeComponent(this);

        LifetimeScope = lifetimeScope;

        Logger = lifetimeScope.Resolve<IAppLogger<WebViewHostBase>>();
        AppContext = lifetimeScope.Resolve<IAppContext>();

        Loaded += OnWebViewHostLoaded;
        WebView2 = new Microsoft.Web.WebView2.Wpf.WebView2();

        Margin = new Thickness(0);
        Padding = new Thickness(0);

        MainGrid = new Grid();
        MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        MainGrid.RowDefinitions.Add(new RowDefinition());

        SetupMenuBar();

        WebView2.SetValue(Grid.RowProperty, 1);
        MainGrid.Children.Add(WebView2);

        Content = MainGrid;
    }

    protected ILifetimeScope LifetimeScope { get; }

    private Grid MainGrid { get; }

    protected HttpClient InnerHttpClient { get; } = new();

    private IAppLogger<WebViewHostBase> Logger { get; }

    private IAppContext AppContext { get; }


    protected CoreWebView2 CoreWebView2 => WebView2.CoreWebView2;

    public CoreWebView2CookieManager CookieManager => CoreWebView2.CookieManager;

    protected virtual IReadOnlyList<string> BuiltInBrowserArguments { get; } =
    [
        "--unsafely-disable-devtools-self-xss-warnings",
        "--webview-disable-safebrowsing-support"
    ];


    /// <summary>
    ///     --ignore-certificate-errors
    ///     --proxy-server=http://localhost:10808
    /// </summary>
    protected List<string> CustomBrowserArguments { get; } = new();


    protected string AdditionalBrowserArguments =>
        string.Join(" ",
            BuiltInBrowserArguments
                .Concat(CustomBrowserArguments)
                .Where(s => !string.IsNullOrWhiteSpace(s)));

    /// <summary>
    ///     !WARNING Not use WebView2CompositionControl !!
    /// </summary>
    public Microsoft.Web.WebView2.Wpf.WebView2 WebView2 { get; }

    private TaskCompletionSource? NavigateTaskCompletionSource { get; set; }

    private bool FirstRending { get; set; } = true;

    protected virtual bool IsApplyFaviconChanged { get; set; }


    public async Task RefreshAsync()
    {
        await NavigateAsync(CoreWebView2.Source);
    }

    public async Task<string> ExecuteScriptAsync(string script)
    {
        return await CoreWebView2.ExecuteScriptAsync(script);
    }

    protected virtual Task<CoreWebView2Environment> GetCoreWebView2Environment()
    {
        var options = new CoreWebView2EnvironmentOptions
        {
            AdditionalBrowserArguments = AdditionalBrowserArguments,
            AreBrowserExtensionsEnabled = true
        };
        return CoreWebView2Environment.CreateAsync(
            null,
            AppContext.GetDefaultWebView2UserDataFolder(),
            options);
    }


    public async Task NavigateAsync(string uri)
    {
        NavigateTaskCompletionSource = new TaskCompletionSource();

        try
        {
            CoreWebView2.Navigate(uri);
            await NavigateTaskCompletionSource.Task;
        }
        catch (Exception e)
        {
            Logger.Error(e);

            //!WARNING Here CoreWebView2 may be empty !!
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (CoreWebView2 != null)
            {
                CoreWebView2.NavigateToString(e.ToString());
            }
            else
            {
                Content = ResolveErrorView(e);
            }
        }

        NavigateTaskCompletionSource = null;
    }

    public async Task NavigateAsync(Uri uri)
    {
        await NavigateAsync(uri.ToString());
    }


    protected virtual async Task OnWebViewHostLoaded()
    {
        if (!FirstRending)
        {
            return;
        }

        FirstRending = false;


        try
        {
            var env = await GetCoreWebView2Environment();
            await WebView2.EnsureCoreWebView2Async(env);

            AttachEvent();

            await InternalWebViewHostLoadedAsync();
        }
        catch (Exception e)
        {
            Logger.Error(e);
            Content = ResolveErrorView(e);
        }
    }

    private IErrorView? ResolveErrorView(Exception e)
    {
        if (Disposing)
        {
            return null;
        }

        return LifetimeScope.Resolve<IErrorView>(new TypedParameter(typeof(Exception), e));
    }

    private async void OnWebViewHostLoaded(object sender, RoutedEventArgs e)
    {
        var dllPath = Path.Combine(
            AppContext.GetMainAppDirectory(),
            "runtimes",
            "win-x64",
            "native",
            "WebView2Loader.dll");
        NativeDllLoaderTools.LoadFrom(dllPath);

        await OnWebViewHostLoaded();
    }

    [DllImport("kernel32", SetLastError = true)]
    public static extern IntPtr LoadLibrary(string lpFileName);

    public void DeleteAllCookies()
    {
        CoreWebView2.CookieManager.DeleteAllCookies();
    }

    public async Task ClearBrowsingDataAsync()
    {
        await CoreWebView2.Profile.ClearBrowsingDataAsync();
    }

    protected virtual Task InternalWebViewHostLoadedAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual Task InternalFaviconChangedAsync(object? sender, string base64)
    {
        return Task.CompletedTask;
    }

    // ReSharper disable once UnusedParameter.Local
    private void EventDebug([CallerMemberName] string caller = "")
    {
        //Logger.Debug($"Event: {caller}");
    }

    public void OpenDevToolsWindow()
    {
        CoreWebView2.OpenDevToolsWindow();
    }

    public static bool TryCreateCookie(
        string cookieString,
        string currentUrl,
        CoreWebView2CookieManager cookieManager,
        out CoreWebView2Cookie? cookie)
    {
        cookie = null;

        var cookieDict = WebView2Tools.ParseCookie(cookieString);

        if (!cookieDict.TryGetValue("Name", out var name) ||
            !cookieDict.TryGetValue("Value", out var cookieValue))
        {
            return false;
        }

        var uri = new Uri(currentUrl);

        var domain = cookieDict.ContainsKey("Domain") ? cookieDict["Domain"] : uri.Host;
        var path = cookieDict.ContainsKey("Path") ? cookieDict["Path"] : "/";

        try
        {
            cookie = cookieManager.CreateCookie(name, cookieValue, domain, path);
        }
        catch
        {
            return false;
        }

        if (cookieDict.TryGetValue("Max-Age", out var maxAgeStr) &&
            int.TryParse(maxAgeStr, out var maxAgeSeconds))
        {
            cookie.Expires = DateTime.UtcNow.AddSeconds(maxAgeSeconds);
        }
        else if (cookieDict.TryGetValue("Expires", out var expiresStr) &&
                 DateTime.TryParse(
                     expiresStr,
                     CultureInfo.InvariantCulture,
                     DateTimeStyles.AdjustToUniversal,
                     out var expires))
        {
            cookie.Expires = expires.ToUniversalTime();
        }

        cookie.IsSecure = cookieDict.ContainsKey("Secure");
        cookie.IsHttpOnly = cookieDict.ContainsKey("HttpOnly");

        if (cookieDict.TryGetValue("SameSite", out var sameSiteStr))
        {
            switch (sameSiteStr.ToLowerInvariant())
            {
                case "strict":
                    cookie.SameSite = CoreWebView2CookieSameSiteKind.Strict;
                    break;
                case "lax":
                    cookie.SameSite = CoreWebView2CookieSameSiteKind.Lax;
                    break;
                case "none":
                    cookie.SameSite = CoreWebView2CookieSameSiteKind.None;
                    break;
                default:
                    cookie.SameSite = CoreWebView2CookieSameSiteKind.None;
                    break;
            }
        }

        return true;
    }


    #region Event

    private void AttachEvent()
    {
        var coreWebView2 = CoreWebView2;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (coreWebView2 == null)
        {
            return;
        }

        coreWebView2.ContainsFullScreenElementChanged += OnContainsFullScreenElementChanged;
        coreWebView2.ContentLoading += OnContentLoading;
        coreWebView2.DocumentTitleChanged += OnDocumentTitleChanged;
        coreWebView2.FrameNavigationCompleted += OnFrameNavigationCompleted;
        coreWebView2.FrameNavigationStarting += OnFrameNavigationStarting;
        coreWebView2.HistoryChanged += OnHistoryChanged;
        coreWebView2.NavigationCompleted += OnNavigationCompleted;
        coreWebView2.NavigationStarting += OnNavigationStarting;
        coreWebView2.NewWindowRequested += OnNewWindowRequested;
        coreWebView2.PermissionRequested += OnPermissionRequested;
        coreWebView2.ProcessFailed += OnProcessFailed;
        coreWebView2.ScriptDialogOpening += OnScriptDialogOpening;
        coreWebView2.SourceChanged += OnSourceChanged;
        coreWebView2.WebMessageReceived += OnWebMessageReceived;
        coreWebView2.WebResourceRequested += OnWebResourceRequested;
        coreWebView2.WindowCloseRequested += OnWindowCloseRequested;
        coreWebView2.BasicAuthenticationRequested += OnBasicAuthenticationRequested;
        coreWebView2.ContextMenuRequested += OnContextMenuRequested;
        coreWebView2.StatusBarTextChanged += OnStatusBarTextChanged;
        coreWebView2.ServerCertificateErrorDetected += OnServerCertificateErrorDetected;
        coreWebView2.FaviconChanged += OnFaviconChanged;
        coreWebView2.LaunchingExternalUriScheme += OnLaunchingExternalUriScheme;
        coreWebView2.DOMContentLoaded += OnDOMContentLoaded;
        coreWebView2.WebResourceResponseReceived += OnWebResourceResponseReceived;
        coreWebView2.SaveAsUIShowing += OnSaveAsUIShowing;
        coreWebView2.ScreenCaptureStarting += OnScreenCaptureStarting;
        coreWebView2.SaveFileSecurityCheckStarting += OnSaveFileSecurityCheckStarting;
        coreWebView2.NotificationReceived += OnNotificationReceived;
        coreWebView2.DownloadStarting += OnDownloadStarting;
        coreWebView2.FrameCreated += OnFrameCreated;
        coreWebView2.ClientCertificateRequested += OnClientCertificateRequested;
        coreWebView2.IsDocumentPlayingAudioChanged += OnIsDocumentPlayingAudioChanged;
        coreWebView2.IsMutedChanged += OnIsMutedChanged;
        coreWebView2.IsDefaultDownloadDialogOpenChanged += OnIsDefaultDownloadDialogOpenChanged;
    }

    private void DetachEvent()
    {
        var coreWebView2 = CoreWebView2;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (coreWebView2 == null)
        {
            return;
        }

        coreWebView2.ContainsFullScreenElementChanged -= OnContainsFullScreenElementChanged;
        coreWebView2.ContentLoading -= OnContentLoading;
        coreWebView2.DocumentTitleChanged -= OnDocumentTitleChanged;
        coreWebView2.FrameNavigationCompleted -= OnFrameNavigationCompleted;
        coreWebView2.FrameNavigationStarting -= OnFrameNavigationStarting;
        coreWebView2.HistoryChanged -= OnHistoryChanged;
        coreWebView2.NavigationCompleted -= OnNavigationCompleted;
        coreWebView2.NavigationStarting -= OnNavigationStarting;
        coreWebView2.NewWindowRequested -= OnNewWindowRequested;
        coreWebView2.PermissionRequested -= OnPermissionRequested;
        coreWebView2.ProcessFailed -= OnProcessFailed;
        coreWebView2.ScriptDialogOpening -= OnScriptDialogOpening;
        coreWebView2.SourceChanged -= OnSourceChanged;
        coreWebView2.WebMessageReceived -= OnWebMessageReceived;
        coreWebView2.WebResourceRequested -= OnWebResourceRequested;
        coreWebView2.WindowCloseRequested -= OnWindowCloseRequested;
        coreWebView2.BasicAuthenticationRequested -= OnBasicAuthenticationRequested;
        coreWebView2.ContextMenuRequested -= OnContextMenuRequested;
        coreWebView2.StatusBarTextChanged -= OnStatusBarTextChanged;
        coreWebView2.ServerCertificateErrorDetected -= OnServerCertificateErrorDetected;
        coreWebView2.FaviconChanged -= OnFaviconChanged;
        coreWebView2.LaunchingExternalUriScheme -= OnLaunchingExternalUriScheme;
        coreWebView2.DOMContentLoaded -= OnDOMContentLoaded;
        coreWebView2.WebResourceResponseReceived -= OnWebResourceResponseReceived;
        coreWebView2.SaveAsUIShowing -= OnSaveAsUIShowing;
        coreWebView2.ScreenCaptureStarting -= OnScreenCaptureStarting;
        coreWebView2.SaveFileSecurityCheckStarting -= OnSaveFileSecurityCheckStarting;
        coreWebView2.NotificationReceived -= OnNotificationReceived;
        coreWebView2.DownloadStarting -= OnDownloadStarting;
        coreWebView2.FrameCreated -= OnFrameCreated;
        coreWebView2.ClientCertificateRequested -= OnClientCertificateRequested;
        coreWebView2.IsDocumentPlayingAudioChanged -= OnIsDocumentPlayingAudioChanged;
        coreWebView2.IsMutedChanged -= OnIsMutedChanged;
        coreWebView2.IsDefaultDownloadDialogOpenChanged -= OnIsDefaultDownloadDialogOpenChanged;
    }


    protected virtual void OnNewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
    {
        EventDebug();
    }

    protected void OnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        EventDebug();

        var source = NavigateTaskCompletionSource;
        if (source != null)
        {
            source.SetResult();
        }

        RaiseMenuBarCommandsCanExecuteChanged();

        InternalOnNavigationCompleted(sender, e);
    }

    public bool IsCoreWebView2Null()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        return CoreWebView2 == null;
    }

    protected virtual void InternalOnNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
    }


    protected virtual void OnNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnBasicAuthenticationRequested(object? sender,
        CoreWebView2BasicAuthenticationRequestedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnClientCertificateRequested(object? sender,
        CoreWebView2ClientCertificateRequestedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnContainsFullScreenElementChanged(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnDOMContentLoaded(object? sender, CoreWebView2DOMContentLoadedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnSaveAsUIShowing(object? sender, CoreWebView2SaveAsUIShowingEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnScreenCaptureStarting(object? sender, CoreWebView2ScreenCaptureStartingEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnSaveFileSecurityCheckStarting(object? sender,
        CoreWebView2SaveFileSecurityCheckStartingEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnNotificationReceived(object? sender, CoreWebView2NotificationReceivedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnDownloadStarting(object? sender, CoreWebView2DownloadStartingEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnFrameCreated(object? sender, CoreWebView2FrameCreatedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnIsDocumentPlayingAudioChanged(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnIsMutedChanged(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnIsDefaultDownloadDialogOpenChanged(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnWebResourceResponseReceived(object? sender,
        CoreWebView2WebResourceResponseReceivedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnLaunchingExternalUriScheme(object? sender,
        CoreWebView2LaunchingExternalUriSchemeEventArgs e)
    {
        EventDebug();
    }

    protected async void OnFaviconChanged(object? sender, object e)
    {
        EventDebug();
        if (!IsApplyFaviconChanged)
        {
            return;
        }

        var uri = CoreWebView2.FaviconUri;
        if (string.IsNullOrEmpty(uri))
        {
            return;
        }

        try
        {
            //!WARNING There may be performance issues(InnerHttpClient)
            var data = await InnerHttpClient.GetByteArrayAsync(uri);
            var base64 = Convert.ToBase64String(data);

            await InternalFaviconChangedAsync(sender, base64);
        }
        catch (Exception ex)
        {
            Logger.Error(ex);
        }
    }

    protected virtual void OnServerCertificateErrorDetected(object? sender,
        CoreWebView2ServerCertificateErrorDetectedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnStatusBarTextChanged(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnContextMenuRequested(object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnWindowCloseRequested(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnWebResourceRequested(object? sender, CoreWebView2WebResourceRequestedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnSourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnScriptDialogOpening(object? sender, CoreWebView2ScriptDialogOpeningEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnProcessFailed(object? sender, CoreWebView2ProcessFailedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnPermissionRequested(object? sender, CoreWebView2PermissionRequestedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnHistoryChanged(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnFrameNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnFrameNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        EventDebug();
    }

    protected virtual void OnDocumentTitleChanged(object? sender, object e)
    {
        EventDebug();
    }

    protected virtual void OnContentLoading(object? sender, CoreWebView2ContentLoadingEventArgs e)
    {
        EventDebug();
    }

    #endregion
}