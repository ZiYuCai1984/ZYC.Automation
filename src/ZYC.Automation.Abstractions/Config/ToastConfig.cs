using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.Config;

public class ToastConfig : IConfig
{
    public int MaxToasts { get; set; } = 7;
}