using ZYC.Automation.Abstractions.Config.Attributes;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.Config;

[Hidden]
public class SideBarConfig : IConfig
{
    public bool IsVisible { get; set; } = false;
}