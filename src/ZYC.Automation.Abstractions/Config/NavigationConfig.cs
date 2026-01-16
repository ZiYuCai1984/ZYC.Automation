using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.Config;

public class NavigationConfig : IConfig
{
    public int MaxHistoryNum { get; set; } = 10;
}