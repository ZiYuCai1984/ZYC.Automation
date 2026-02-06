using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Modules.Secrets.Abstractions;

public static class SecretsModuleConstants
{
    public const string Icon = "🔑";

    public const string Host = "secrets";

    public const string Title = "Secrets";

    public static Uri Uri => UriTools.CreateAppUri(Host);
}