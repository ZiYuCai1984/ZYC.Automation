using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.Config;

public class StatusBarConfig : IConfig
{
    public bool IsVisible { get; set; } = true;
}