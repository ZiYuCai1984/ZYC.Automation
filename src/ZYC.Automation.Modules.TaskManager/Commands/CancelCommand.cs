using Autofac;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager.Commands;

[RegisterSingleInstanceAs(typeof(CancelCommand), typeof(ITaskOperationCommand), PreserveExistingDefaults = true)]
internal class CancelCommand : AsyncCommandBase<Guid?>, ITaskOperationCommand
{
    public CancelCommand(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    private ITaskManager TaskManager => LifetimeScope.Resolve<ITaskManager>();

    protected override bool InternalCanExecute(Guid? taskId)
    {
        if (taskId == null)
        {
            DebuggerTools.Break();
            return false;
        }

        if (!TaskManager.TryGetTask(taskId.Value, out var task))
        {
            return false;
        }

        return task.Snapshot.State == ManagedTaskState.Paused
               || task.Snapshot.State == ManagedTaskState.Running
               || task.Snapshot.State == ManagedTaskState.Queuing;
    }

    protected override async Task InternalExecuteAsync(Guid? taskId)
    {
        if (taskId == null)
        {
            DebuggerTools.Break();
            return;
        }

        if (TaskManager.TryGetTask(taskId.Value, out var task))
        {
            await task.CancelAsync();
        }

        LifetimeScope.RaiseTaskOperationCommandsCanExecuteChanged();
    }
}