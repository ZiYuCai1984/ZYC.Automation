using System.Windows.Input;

namespace ZYC.Automation.Abstractions.QuickBar;

public interface IQuickBarItem
{
    string Icon { get; }

    ICommand Command { get; }

    string Tooltip { get; }
}