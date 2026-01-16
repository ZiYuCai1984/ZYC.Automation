using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.ModuleManager.Abstractions;

/// <summary>
///     Configuration for the NuGet module manager.
/// </summary>
public class NuGetModuleManagerConfig : IConfig
{
    /// <summary>
    ///     Gets or sets the NuGet package source.
    /// </summary>
    public string Source { get; set; } = "https://api.nuget.org/v3/index.json";

    /// <summary>
    ///     Gets or sets the search term used for module discovery.
    /// </summary>
    public string SearchTerm { get; set; } = "ZYC.Automation.Modules";
}