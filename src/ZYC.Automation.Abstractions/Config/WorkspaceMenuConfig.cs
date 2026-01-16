using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.Config;

public class WorkspaceMenuConfig : IConfig
{
    public bool IsVisible { get; set; } = true;
}