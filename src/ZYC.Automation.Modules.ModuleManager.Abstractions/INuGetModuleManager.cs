namespace ZYC.Automation.Modules.ModuleManager.Abstractions;

/// <summary>
///     Manages NuGet-based modules and their installation lifecycle.
/// </summary>
public interface INuGetModuleManager
{
    /// <summary>
    ///     Gets available NuGet modules.
    /// </summary>
    /// <param name="token">The cancellation token.</param>
    /// <returns>An array of available modules.</returns>
    Task<INuGetModule[]> GetModulesAsync(CancellationToken token);

    /// <summary>
    ///     Installs the specified NuGet module.
    /// </summary>
    /// <param name="module">The module to install.</param>
    /// <param name="token">The cancellation token.</param>
    Task InstallAsync(INuGetModule module, CancellationToken token);

    /// <summary>
    ///     Uninstalls the specified NuGet module.
    /// </summary>
    /// <param name="module">The module to uninstall.</param>
    void Uninstall(INuGetModule module);
}