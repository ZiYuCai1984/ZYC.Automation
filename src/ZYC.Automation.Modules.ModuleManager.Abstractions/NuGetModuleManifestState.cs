using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.ModuleManager.Abstractions;

/// <summary>
///     Tracks installed NuGet modules and their manifest files.
/// </summary>
public class NuGetModuleManifestState : IState
{
    /// <summary>
    ///     Gets or sets the installed modules tracked by the manifest.
    /// </summary>
    public InstalledModule[] InstalledModules { get; set; } = [];

    /// <summary>
    ///     Represents a module recorded in the manifest.
    /// </summary>
    public class InstalledModule
    {
        /// <summary>
        ///     Gets or sets the NuGet package identifier.
        /// </summary>
        public string PackageId { get; set; } = "";

        /// <summary>
        ///     Gets or sets the package version.
        /// </summary>
        public string Version { get; set; } = "";

        /// <summary>
        ///     Gets or sets the installed files for the package.
        /// </summary>
        public string[] Files { get; set; } = [];
    }
}