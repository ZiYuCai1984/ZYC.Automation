using Autofac;
using ZYC.Automation.Abstractions.State;
using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class
    UnlockTabItemPairCommand : ParameterPairCommandBase<LockTabItemPairCommand, UnlockTabItemPairCommand,
    ITabItemInstance?>
{
    private TabItemLockState? _tabItemLockState;

    public UnlockTabItemPairCommand(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    private TabItemLockState TabItemLockState => _tabItemLockState ??= LifetimeScope.Resolve<TabItemLockState>();


    protected override void InternalExecute(ITabItemInstance? instance)
    {
        base.InternalExecute(instance);

        if (instance == null)
        {
            return;
        }

        TabItemLockState.TabItems =
            TabItemLockState.TabItems = TabItemLockState.TabItems.RemoveReference(instance.TabReference);
    }

    protected override bool InternalCanExecute(ITabItemInstance? instance)
    {
        if (instance == null)
        {
            return false;
        }

        var result = TabItemLockState.TabItems.Contains(
            instance.TabReference);
        return result;
    }
}