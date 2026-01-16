using System.Diagnostics;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.Commands;

[RegisterSingleInstance]
internal class DownloadCommand : AsyncCommandBase
{
    public DownloadCommand(ITaskManager taskManager, IUpdateManager updateManager)
    {
        TaskManager = taskManager;
        UpdateManager = updateManager;
    }

    private ITaskManager TaskManager { get; }
    private IUpdateManager UpdateManager { get; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        var product = UpdateManager.GetCurrentUpdateContext().NewProduct;
        Debug.Assert(product != null);

        var managedTask = TaskManager.Enqueue(
            UpdateTaskProvider.ProviderId,
            DownloadNewProductDefinition.DefinitionId, 1, product.ToJsonText());

        await managedTask.Completion;
    }

    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting;
    }
}