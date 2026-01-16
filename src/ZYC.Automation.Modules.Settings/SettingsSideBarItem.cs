using System.Windows.Input;
using Autofac;
using ZYC.Automation.Abstractions.SideBar;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Settings;

[RegisterSingleInstance]
internal class SettingsSideBarItem : IDefaultSideBarItem
{
    public SettingsSideBarItem(ILifetimeScope lifetimeScope)
    {
        Command = lifetimeScope.CreateNavigateCommand(SettingsTabItem.Constants.Uri);
    }


    public string Icon => SettingsTabItem.Constants.Icon;

    public ICommand Command { get; }

    public SideBarSection Section => SideBarSection.Bottom;
}