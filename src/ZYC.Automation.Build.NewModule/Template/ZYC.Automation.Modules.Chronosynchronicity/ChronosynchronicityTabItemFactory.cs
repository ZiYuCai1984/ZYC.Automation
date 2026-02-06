using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Build.NewModule.Template.ZYC.Automation.Modules.Chronosynchronicity.Abstractions;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

// ReSharper disable once CheckNamespace
namespace ZYC.Automation.Modules.Chronosynchronicity;

[RegisterSingleInstance]
internal class ChronosynchronicityTabItemFactory : ITabItemFactory
{
    public bool IsSingle => true;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return context.Resolve<ChronosynchronicityTabItem>(
            new TypedParameter(typeof(TabReference), new TabReference(context.Uri)));
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == ChronosynchronicityModuleConstants.Host)
        {
            return true;
        }

        return false;
    }
}