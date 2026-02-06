using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Settings.Abstractions;
using ZYC.Automation.Modules.Settings.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Settings;

[Register]
[ConstantsSource(typeof(SettingsModuleConstants))]
internal class SettingsTabItem : TabItemInstanceBase<SettingsView>
{
    public SettingsTabItem(ILifetimeScope lifetimeScope, TabReference tabReference) : base(lifetimeScope, tabReference)
    {
    }
}