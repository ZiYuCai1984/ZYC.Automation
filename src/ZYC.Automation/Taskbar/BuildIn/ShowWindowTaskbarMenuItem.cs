using System.Windows.Input;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Abstractions.TaskbarMenu;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Taskbar.BuildIn;

[RegisterSingleInstance]
internal class ShowWindowTaskbarMenuItem : ITaskbarMenuItem
{
    public ShowWindowTaskbarMenuItem(ShowWindowCommand showWindowCommand)
    {
        ShowWindowCommand = showWindowCommand;
        Info = new MenuItemInfo
        {
            Title = "Show window"
        };
    }

    private ShowWindowCommand ShowWindowCommand { get; }

    public MenuItemInfo Info { get; }

    public ICommand Command => ShowWindowCommand;

    public ITaskbarMenuItem[] SubItems { get; } = [];
}