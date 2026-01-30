using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Modules.WebBrowser.Abstractions;

public static class WebBrowserModuleConstants
{
    public const string MenuIcon = "Web";

    public const string Host = UriTools.TempHost;

    public const string MenuTitle = "WebBrowser";

    public static Uri Uri => UriTools.CreateAppUri(Host);
}