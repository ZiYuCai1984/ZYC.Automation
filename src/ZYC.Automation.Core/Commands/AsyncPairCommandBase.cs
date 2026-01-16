using System.Windows.Input;
using Autofac;
using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Core.Commands;

public abstract class AsyncPairCommandBase<T1, T2> : AsyncCommandBase, IPairCommand
    where T1 : AsyncPairCommandBase<T1, T2>
    where T2 : AsyncPairCommandBase<T1, T2>
{
    private T1? _command1;

    private T2? _command2;

    protected AsyncPairCommandBase(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    protected ILifetimeScope LifetimeScope { get; }

    public T1 Command1 => _command1 ??= LifetimeScope.Resolve<T1>();

    public T2 Command2 => _command2 ??= LifetimeScope.Resolve<T2>();

    public ICommand Self => this;

    public void RaisePairCommandsChanged()
    {
        Command1.OnPropertyChanged(nameof(Self));
        Command1.RaiseCanExecuteChanged();

        Command2.OnPropertyChanged(nameof(Self));
        Command2.RaiseCanExecuteChanged();
    }

    public new async Task ExecuteAsync(object? parameter)
    {
        IsExecuting = true;
        try
        {
            await InternalExecuteAsync(parameter);
        }
        finally
        {
            IsExecuting = false;
        }

        RaiseExecuted();
        RaisePairCommandsChanged();
    }
}