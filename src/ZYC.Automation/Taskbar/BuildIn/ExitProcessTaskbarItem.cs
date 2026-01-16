using System.Windows.Input;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Abstractions.TaskbarMenu;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Taskbar.BuildIn;

[RegisterSingleInstance]
internal class ExitProcessTaskbarItem : ITaskbarMenuItem
{
    public ExitProcessTaskbarItem(ExitProcessCommand exitProcessCommand)
    {
        ExitProcessCommand = exitProcessCommand;

        Info = new MenuItemInfo
        {
            Title = "Exit"
        };
    }

    private ExitProcessCommand ExitProcessCommand { get; }

    public MenuItemInfo Info { get; }

    public ICommand Command => ExitProcessCommand;

    public ITaskbarMenuItem[] SubItems { get; } = [];
}