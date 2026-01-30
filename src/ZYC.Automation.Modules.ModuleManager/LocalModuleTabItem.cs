using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.Automation.Modules.ModuleManager.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[Register]
[ConstantsSource(typeof(ModuleManagerModuleConstants.Local))]
internal class LocalModuleTabItem : TabItemInstanceBase<LocalModuleManagerView>
{
    public LocalModuleTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
}