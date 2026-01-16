using System.Windows.Input;

namespace ZYC.Automation.Abstractions.SideBar;

public interface IDefaultSideBarItem : ISideBarItem
{
    string Icon { get; }

    ICommand Command { get; }

    object ISideBarItem.View => throw new InvalidOperationException();
}