using System.Windows.Input;
using ZYC.Automation.Abstractions.MainMenu;

namespace ZYC.Automation.Abstractions.TaskbarMenu;

public interface ITaskbarMenuItem
{
    MenuItemInfo Info { get; }

    ICommand Command { get; }

    ITaskbarMenuItem[] SubItems { get; }
}