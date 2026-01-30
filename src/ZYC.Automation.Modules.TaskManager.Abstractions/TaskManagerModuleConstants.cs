using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Modules.TaskManager.Abstractions;

public static class TaskManagerModuleConstants
{
    public const string Icon = "DnsOutline";

    public const string Host = "taskmanager";

    public const string Title = "TaskManager";

    public static Uri Uri => UriTools.CreateAppUri(Host);
}