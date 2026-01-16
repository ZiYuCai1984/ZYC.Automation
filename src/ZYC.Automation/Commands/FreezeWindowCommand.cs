using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.State;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Commands;

[RegisterSingleInstance]
internal class FreezeWindowCommand : PairCommandBase<FreezeWindowCommand, UnfreezeWindowCommand>
{
    public FreezeWindowCommand(
        ILifetimeScope lifetimeScope, DesktopWindowState desktopWindowState) : base(
        lifetimeScope)
    {
        DesktopWindowState = desktopWindowState;
    }

    private DesktopWindowState DesktopWindowState { get; }

    protected override void InternalExecute(object? parameter)
    {
        LifetimeScope.Resolve<IMainWindow>().SetIsFrozen(true);
    }

    public override bool CanExecute(object? parameter)
    {
        return !DesktopWindowState.IsFrozen;
    }
}