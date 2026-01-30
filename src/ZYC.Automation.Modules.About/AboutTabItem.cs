using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.About.Abstractions;
using ZYC.Automation.Modules.About.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.About;

[Register]
[ConstantsSource(typeof(AboutModuleConstants))]
internal class AboutTabItem : TabItemInstanceBase<AboutView>
{
    public AboutTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
}