using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Modules.Settings.Abstractions;

public static class SettingsModuleConstants
{
    public static string Icon => "ApplicationCogOutline";

    public static string Host => "settings";

    public static string Title => "ApplicationSettings";

    public static Uri Uri => UriTools.CreateAppUri(Host);
}