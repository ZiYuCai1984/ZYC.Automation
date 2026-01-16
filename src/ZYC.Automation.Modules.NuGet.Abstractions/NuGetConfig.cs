using ZYC.Automation.Abstractions.Config.Attributes;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.NuGet.Abstractions;

/// <summary>
///     Defines NuGet configuration settings for module updates.
/// </summary>
public class NuGetConfig : IConfig
{
    /// <summary>
    ///     Gets or sets the NuGet sources used for updates.
    /// </summary>
    public string[] Sources { get; set; } = ["https://api.nuget.org/v3/index.json"];

    /// <summary>
    ///     Gets or sets the relative cache folder for NuGet packages.
    /// </summary>
    public string CacheFolder { get; set; } = ".cache";
}