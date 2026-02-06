using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.CLI.Abstractions;
using ZYC.Automation.Modules.CLI.UI;
using ZYC.CoreToolkit.Common;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI;

[Register]
[ConstantsSource(typeof(CLIModuleConstants))]
internal class CLITabItem : TabItemInstanceBase
{
    public CLITabItem(
        ILifetimeScope lifetimeScope,
        TabReference tabReference,
        CLITabItemIndexPool indexPool) : base(lifetimeScope, tabReference)
    {
        IndexPool = indexPool;
        Lease = IndexPool.AcquireLease();
    }

    public override object View => _view ??= LifetimeScope.Resolve<CLIView>(
        new TypedParameter(typeof(CLIUriOptions), CLIUriOptions.Parse(TabReference.Uri)));

    private CLITabItemIndexPool IndexPool { get; }

    private IndexPool.Lease Lease { get; }

    public override string Title => $"{CLIModuleConstants.DefaultTitle} - {Lease.Index}";

    public override string Icon => CLIModuleConstants.Icon;

    public override bool Localization => false;

    public override void Dispose()
    {
        base.Dispose();
        Lease.Dispose();
    }
}