using ZYC.Automation.Modules.TaskManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Mock.UI;

[Register]
public partial class TestTaskManagerView
{
    public TestTaskManagerView(ITaskManager taskManager)
    {
        TaskManager = taskManager;

        InitializeComponent();
    }

    private ITaskManager TaskManager { get; }
}