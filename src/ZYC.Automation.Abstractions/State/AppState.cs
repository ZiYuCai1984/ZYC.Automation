using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.State;

public class AppState : IState
{
    public StartupTarget StartupTarget { get; set; } = StartupTarget.Main;
}