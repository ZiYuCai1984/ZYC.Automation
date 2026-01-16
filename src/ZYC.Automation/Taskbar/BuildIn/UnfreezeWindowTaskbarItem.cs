using System.Windows.Input;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Abstractions.TaskbarMenu;
using ZYC.Automation.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Taskbar.BuildIn;

[RegisterSingleInstance]
internal class UnfreezeWindowTaskbarItem : ITaskbarMenuItem
{
    public UnfreezeWindowTaskbarItem(UnfreezeWindowCommand unFreezeWindowCommand)
    {
        UnfreezeWindowCommand = unFreezeWindowCommand;

        Info = new MenuItemInfo
        {
            Title = "Unfreeze"
        };
    }

    private UnfreezeWindowCommand UnfreezeWindowCommand { get; }

    public MenuItemInfo Info { get; }

    public ICommand Command => UnfreezeWindowCommand;

    public ITaskbarMenuItem[] SubItems { get; } = [];
}