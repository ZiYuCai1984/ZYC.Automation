using System.IO;
using System.IO.Compression;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.Automation.Modules.NuGet.Abstractions;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[RegisterSingleInstanceAs(typeof(INuGetModuleManager))]
internal class NuGetModuleManager : INuGetModuleManager
{
    public NuGetModuleManager(
        IPendingDeleteManager pendingDeleteManager,
        INuGetManager nuGetManager,
        NuGetModuleManagerConfig config,
        NuGetModuleManifestState manifestState,
        IAppContext appContext)
    {
        PendingDeleteManager = pendingDeleteManager;
        NuGetManager = nuGetManager;
        Config = config;
        ManifestState = manifestState;
        AppContext = appContext;
    }

    private IPendingDeleteManager PendingDeleteManager { get; }
    private INuGetManager NuGetManager { get; }
    private NuGetModuleManagerConfig Config { get; }
    private NuGetModuleManifestState ManifestState { get; }
    private IAppContext AppContext { get; }

    public async Task<INuGetModule[]> GetModulesAsync(CancellationToken token)
    {
        var source = new PackageSource(Config.Source);
        var repository = Repository.Factory.GetCoreV3(source);
        var search = await repository.GetResourceAsync<PackageSearchResource>(token);
        var filter = new SearchFilter(true)
        {
            //TODO OrderBy = SearchOrderBy.Popularity ??
            //OrderBy = SearchOrderBy.Popularity
        };
        var results = await search.SearchAsync(
            Config.SearchTerm,
            filter,
            0,
            50,
            NullLogger.Instance,
            token);

        var modules = new List<INuGetModule>();
        foreach (var result in results)
        {
            var installed = ManifestState.InstalledModules.Any(m => m.PackageId == result.Identity.Id);
            modules.Add(new NuGetModule(
                result.Identity.Id,
                result.Identity.Version.ToNormalizedString(),
                result.Description ?? string.Empty,
                installed));
        }

        return modules.ToArray();
    }

    public async Task InstallAsync(INuGetModule module, CancellationToken token)
    {
        var processed = new HashSet<string>();
        await NuGetManager.DownloadPackageAndDependenciesRecursiveAsync(
            module.PackageId,
            module.Version,
            processed,
            token);

        var packagePath = NuGetManager.GetNuGetPackageCacheFilePath(module.PackageId, module.Version);
        var extractFolder = AppContext.GetCurrentDirectory();
        var files = await ExtractAsync(packagePath, extractFolder);

        var list = ManifestState.InstalledModules.ToList();
        list.Add(new NuGetModuleManifestState.InstalledModule
        {
            PackageId = module.PackageId,
            Version = module.Version,
            Files = files.ToArray()
        });
        ManifestState.InstalledModules = list.ToArray();
    }

    public void Uninstall(INuGetModule module)
    {
        var record = ManifestState.InstalledModules.FirstOrDefault(m => m.PackageId == module.PackageId);
        if (record == null)
        {
            return;
        }

        foreach (var file in record.Files)
        {
            var path = Path.Combine(AppContext.GetCurrentDirectory(), file);
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch
                {
                    PendingDeleteManager.Add(file);
                }
            }
        }

        var list = ManifestState.InstalledModules.ToList();
        list.Remove(record);
        ManifestState.InstalledModules = list.ToArray();

        var nugetModule = (NuGetModule)module;
        nugetModule.Installed = false;
    }

    /// <summary>
    ///     TODO What happens when encountering the same currently occupied assembly during decompression?
    /// </summary>
    /// <param name="zipPath"></param>
    /// <param name="folder"></param>
    /// <returns></returns>
    private static async Task<List<string>> ExtractAsync(string zipPath, string folder)
    {
        var files = new List<string>();
        await Task.Run(() =>
        {
            try
            {
                using var archive = ZipFile.OpenRead(zipPath);
                foreach (var entry in archive.Entries)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(entry.Name))
                        {
                            continue;
                        }

                        var dest = Path.Combine(folder, entry.FullName);
                        Directory.CreateDirectory(Path.GetDirectoryName(dest)!);
                        entry.ExtractToFile(dest, true);
                        files.Add(entry.FullName);
                    }
                    catch
                    {
                        //ignore
                    }
                }
            }
            catch (Exception)
            {
                DebuggerTools.Break();
            }
        });
        return files;
    }
}