using Autofac;
using ZYC.Automation.Abstractions.State;
using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class
    LockTabItemPairCommand : ParameterPairCommandBase<LockTabItemPairCommand, UnlockTabItemPairCommand, ITabItemInstance
    ?>
{
    private TabItemLockState? _tabItemLockState;

    public LockTabItemPairCommand(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    private TabItemLockState TabItemLockState => _tabItemLockState ??= LifetimeScope.Resolve<TabItemLockState>();


    protected override bool InternalCanExecute(ITabItemInstance? instance)
    {
        if (instance == null)
        {
            return false;
        }


        var reference = instance.TabReference;

        var result = !TabItemLockState.TabItems.Contains(reference);
        return result;
    }

    protected override void InternalExecute(ITabItemInstance? instance)
    {
        base.InternalExecute(instance);

        if (instance == null)
        {
            return;
        }


        TabItemLockState.TabItems =
            TabItemLockState.TabItems.AddReference(instance.TabReference);

        RaisePairCommandsChanged();
    }
}