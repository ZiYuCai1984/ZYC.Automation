using System.Text.Json;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Mock.Commands;

[RegisterSingleInstance]
internal class CountTaskCommand : CommandBase<int>
{
    public CountTaskCommand(ITaskManager taskManager)
    {
        TaskManager = taskManager;
    }

    private ITaskManager TaskManager { get; }

    protected override void InternalExecute(int parameter)
    {
        var payload = JsonSerializer.Serialize(new { Steps = 60, DelayMs = 100 });
        for (var i = 0; i < parameter; ++i)
        {
            TaskManager.Enqueue("mock", "mock/count", 1, payload);
        }
    }
}