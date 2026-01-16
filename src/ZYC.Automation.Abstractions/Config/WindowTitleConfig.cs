using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.Config;

public class WindowTitleConfig : IConfig
{
    public bool IsMachineNameVisible { get; set; } = true;

    public bool IsVisible { get; set; } = true;
}