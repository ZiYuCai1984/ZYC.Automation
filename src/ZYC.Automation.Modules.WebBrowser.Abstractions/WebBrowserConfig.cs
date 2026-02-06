using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.WebBrowser.Abstractions;

public class WebBrowserConfig : IConfig
{
    public string StartupUri { get; set; } = "https://google.com";
}