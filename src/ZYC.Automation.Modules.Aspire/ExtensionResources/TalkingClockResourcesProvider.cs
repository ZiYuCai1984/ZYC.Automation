using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.ExtensionResources;

[RegisterSingleInstanceAs(typeof(IExtensionResourcesProvider), PreserveExistingDefaults = true)]
internal class TalkingClockResourcesProvider : ResourcesProviderBase
{
    public override void ConfigureResources(IDistributedApplicationBuilder builder)
    {
        builder.AddTalkingClock("TalkingClock");
    }
}