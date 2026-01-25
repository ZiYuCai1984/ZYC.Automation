using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Modules.CLI.UI;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Common;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI;

[Register]
internal class CLITabItem : ITabItemInstance
{
    private object? _view;

    public CLITabItem(
        ILifetimeScope lifetimeScope,
        CLITabItemIndexPool indexPool,
        Uri uri)
    {
        LifetimeScope = lifetimeScope;
        IndexPool = indexPool;
        Lease = IndexPool.AcquireLease();

        TabReference = new TabReference(uri);
    }

    private ILifetimeScope LifetimeScope { get; }

    private CLITabItemIndexPool IndexPool { get; }

    private IndexPool.Lease Lease { get; }

    public TabReference TabReference { get; }

    public Guid Id => TabReference.Id;

    public Uri Uri => TabReference.Uri;

    public string Scheme => Uri.Scheme;

    public string Host => Uri.Host;

    public string Title => $"{Constants.DefaultTitle} - {Lease.Index}";

    public string Icon => Constants.Icon;

    public object View => _view ??= LifetimeScope.Resolve<CLIView>(
        new TypedParameter(typeof(CLIUriOptions), CLIUriOptions.Parse(Uri)));

    public bool Localization => false;

    public Task LoadAsync()
    {
        return Task.CompletedTask;
    }

    public bool OnClosing()
    {
        return true;
    }


    public void Dispose()
    {
        Lease.Dispose();
        View.TryDispose();
    }

    public class Constants
    {
        public static string Host => "cli";

        public static string DefaultTitle => "CLI";

        public static string Icon => "Console";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}