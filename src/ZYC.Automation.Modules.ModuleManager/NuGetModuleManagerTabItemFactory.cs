using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[RegisterSingleInstance]
[TabItemRoute(Host = ModuleManagerModuleConstants.Host, Path = ModuleManagerModuleConstants.NuGet.Path)]
internal class NuGetModuleManagerTabItemFactory : TabItemFactoryBase
{
    public override async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return context.Resolve<NuGetModuleTabItem>(
            new TypedParameter(typeof(TabReference), new TabReference(context.Uri)));
    }
}