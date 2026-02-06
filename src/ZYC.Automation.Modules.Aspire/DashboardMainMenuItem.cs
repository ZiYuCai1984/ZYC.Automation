using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire;

[RegisterSingleInstance]
internal class DashboardMainMenuItem : MainMenuItem
{
    public DashboardMainMenuItem(NavigateToDashboardCommand navigateToDashboardCommand)
    {
        Info = new MenuItemInfo
        {
            Title = "Dashboard",
            Icon = AspireModuleContansts.Icon
        };

        Command = navigateToDashboardCommand;
    }
}