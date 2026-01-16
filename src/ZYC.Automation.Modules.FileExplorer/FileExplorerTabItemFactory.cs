using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.FileExplorer;

[RegisterSingleInstance]
internal class FileExplorerTabItemFactory : ITabItemFactory
{
    public FileExplorerTabItemFactory(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    public bool IsSingle => false;

    public async Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context)
    {
        await Task.CompletedTask;
        return context.Resolve<FileExplorerTabItem>(
            new TypedParameter(typeof(Uri), context.Uri));
    }

    public async Task<bool> CheckUriMatchedAsync(Uri uri)
    {
        await Task.CompletedTask;
        if (uri.Scheme == "file")
        {
            return true;
        }

        return false;
    }
}