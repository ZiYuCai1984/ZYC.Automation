using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Secrets.Abstractions;
using ZYC.Automation.Modules.Secrets.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Secrets;

[Register]
[ConstantsSource(typeof(SecretsModuleConstants))]
internal class SecretsTabItem : TabItemInstanceBase<SecretsManagerView>
{
    public SecretsTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }
}