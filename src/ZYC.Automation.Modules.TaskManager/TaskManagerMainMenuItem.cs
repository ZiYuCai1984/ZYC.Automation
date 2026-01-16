using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager;

[RegisterSingleInstance]
internal class TaskManagerMainMenuItem : MainMenuItem
{
    public TaskManagerMainMenuItem(ILifetimeScope lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = TaskManagerTabItem.Constants.Title,
            Icon = TaskManagerTabItem.Constants.Icon,
            Anchor = MainMenuAnchors.Setting_Manager
        };

        Command = lifetimeScope.CreateNavigateCommand(TaskManagerTabItem.Constants.Uri);
    }
}