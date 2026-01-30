using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.CLI.UI;
using ZYC.CoreToolkit.Common;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI;

[Register]
internal class CLITabItem : TabItemInstanceBase
{
    public CLITabItem(
        ILifetimeScope lifetimeScope,
        Uri uri,
        CLITabItemIndexPool indexPool) : base(lifetimeScope)
    {
        IndexPool = indexPool;
        Lease = IndexPool.AcquireLease();
    }

    public override object View => _view ??= LifetimeScope.Resolve<CLIView>(
        new TypedParameter(typeof(CLIUriOptions), CLIUriOptions.Parse(TabReference.Uri)));

    private CLITabItemIndexPool IndexPool { get; }

    private IndexPool.Lease Lease { get; }

    public override string Title => $"{Constants.DefaultTitle} - {Lease.Index}";

    public override string Icon => Constants.Icon;

    public override bool Localization => false;

    public override void Dispose()
    {
        base.Dispose();

        Lease.Dispose();
    }

    public class Constants
    {
        public static string Host => "cli";

        public static string DefaultTitle => "CLI";

        public static string Icon => "Console";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}