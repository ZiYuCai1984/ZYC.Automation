using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire;

[Register]
[ConstantsSource(typeof(AspireModuleContansts))]
internal class AspireTabItem : TabItemInstanceBase<AspireView>
{
    public AspireTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public override bool Localization => false;
}