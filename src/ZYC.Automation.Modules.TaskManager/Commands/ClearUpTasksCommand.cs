using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.TaskManager.Commands;

[RegisterSingleInstance]
internal class ClearUpTasksCommand : AsyncCommandBase
{
    public ClearUpTasksCommand(ITaskManager taskManager)
    {
        TaskManager = taskManager;
    }

    private ITaskManager TaskManager { get; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        await TaskManager.ClearUpTasksAsync();
    }
}