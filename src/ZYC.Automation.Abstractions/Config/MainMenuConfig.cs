using PropertyChanged;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.Config;

[AddINotifyPropertyChangedInterface]
public class MainMenuConfig : IConfig
{
    public bool IsVisible { get; set; } = true;
}