using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.Automation.Modules.Update.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update;

[Register]
[ConstantsSource(typeof(UpdateModuleConstants))]
internal class UpdateTabItem : TabItemInstanceBase<UpdateView>
{
    public UpdateTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
}