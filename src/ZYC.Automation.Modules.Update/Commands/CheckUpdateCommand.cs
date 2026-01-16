using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.Automation.Modules.Update.Abstractions.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.Commands;

[RegisterSingleInstanceAs(typeof(CheckUpdateCommand), typeof(ICheckUpdateCommand))]
internal class CheckUpdateCommand : CommandBase, ICheckUpdateCommand
{
    public CheckUpdateCommand(
        ITaskManager taskManager,
        IUpdateManager updateManager)
    {
        TaskManager = taskManager;
        UpdateManager = updateManager;
    }

    private ITaskManager TaskManager { get; }

    private IUpdateManager UpdateManager { get; }

    protected override void InternalExecute(object? parameter)
    {
        TaskManager.Enqueue(
            UpdateTaskProvider.ProviderId,
            CheckUpdateTaskDefinition.DefinitionId,
            1,
            "");
    }
}