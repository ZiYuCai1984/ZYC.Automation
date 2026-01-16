using System.Windows.Input;

namespace ZYC.Automation.Abstractions.WindowTitle;

public interface IWindowTitleItem
{
    string Icon { get; }

    ICommand Command { get; }

    bool IsVisible { get; }
}