using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.Language.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Language;

[RegisterSingleInstance]
internal class LanguageTabItemFactory : ITabItemFactory
{
    public LanguageTabItemFactory(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    public bool IsSingle => true;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;

        return context.Resolve<LanguageTabItem>(
            new TypedParameter(typeof(TabReference), new TabReference(context.Uri)));
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Host == LanguageModuleConstants.Host)
        {
            return true;
        }

        return false;
    }
}