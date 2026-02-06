using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.BlazorDemo.Abstractions;
using ZYC.Automation.Modules.BlazorDemo.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.BlazorDemo;

[Register]
[ConstantsSource(typeof(BlazorDemoModuleConstants))]
internal class BlazorDemoTabItem : TabItemInstanceBase<BlazorDemoView>
{
    public BlazorDemoTabItem(
        ILifetimeScope lifetimeScope, 
        TabReference tabReference) : base(lifetimeScope, tabReference)
    {
    }

    public override bool Localization => false;
}