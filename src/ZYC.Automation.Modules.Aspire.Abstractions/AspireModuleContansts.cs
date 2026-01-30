using ZYC.Automation.Abstractions;

namespace ZYC.Automation.Modules.Aspire.Abstractions;

public static class AspireModuleContansts
{
    public const string Host = "aspire";

    public const string Title = "Aspire";

    public const string Icon = Base64IconResources.Aspire;

    public static Uri Uri => UriTools.CreateAppUri(Host);
}