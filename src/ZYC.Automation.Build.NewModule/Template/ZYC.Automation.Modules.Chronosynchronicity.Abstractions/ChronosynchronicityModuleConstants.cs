using MahApps.Metro.IconPacks;
using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Build.NewModule.Template.ZYC.Automation.Modules.Chronosynchronicity.Abstractions;

public static class ChronosynchronicityModuleConstants
{
    public static string Host => "chronosynchronicity";

    public static string Title => "Chronosynchronicity";

    public static string Icon => nameof(PackIconMaterialKind.Bug);

    public static Uri Uri => UriTools.CreateAppUri(Host);
}