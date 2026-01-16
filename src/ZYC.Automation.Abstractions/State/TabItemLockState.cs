using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Abstractions.State;

public class TabItemLockState : IState
{
    public TabReference[] TabItems { get; set; } = [];
}