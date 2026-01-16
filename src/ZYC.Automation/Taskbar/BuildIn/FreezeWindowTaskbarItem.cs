using System.Windows.Input;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Abstractions.TaskbarMenu;
using ZYC.Automation.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Taskbar.BuildIn;

[RegisterSingleInstance]
internal class FreezeWindowTaskbarItem : ITaskbarMenuItem
{
    public FreezeWindowTaskbarItem(FreezeWindowCommand freezeWindowCommand)
    {
        FreezeWindowCommand = freezeWindowCommand;

        Info = new MenuItemInfo
        {
            Title = "Freeze"
        };
    }

    private FreezeWindowCommand FreezeWindowCommand { get; }

    public MenuItemInfo Info { get; }

    public ICommand Command => FreezeWindowCommand;

    public ITaskbarMenuItem[] SubItems { get; } = [];
}