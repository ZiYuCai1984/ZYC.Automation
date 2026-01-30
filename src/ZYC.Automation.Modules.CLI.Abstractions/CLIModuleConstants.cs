using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Modules.CLI.Abstractions;

public static class CLIModuleConstants
{
    public const string Host = "cli";

    public const string DefaultTitle = "CLI";

    public const string Icon = "Console";

    public static Uri Uri => UriTools.CreateAppUri(Host);
}