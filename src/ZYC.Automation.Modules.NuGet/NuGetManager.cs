using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Xml.Linq;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Modules.NuGet.Abstractions;
using ZYC.CoreToolkit.Dotnet;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.NuGet;

[RegisterSingleInstanceAs(typeof(INuGetManager))]
internal class NuGetManager : INuGetManager
{
    public NuGetManager(IAppContext appContext, NuGetConfig config)
    {
        AppContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        NuGetConfig = config ?? throw new ArgumentNullException(nameof(config));

        SourceCacheContext = new NullSourceCacheContext
        {
            DirectDownload = true
        };

        if (NuGetConfig.Sources != null)
        {
            foreach (var source in NuGetConfig.Sources)
            {
                if (string.IsNullOrWhiteSpace(source))
                {
                    continue;
                }

                NuGetSources[source] = new NuGetSource(source);
            }
        }

        if (NuGetSources.Count == 0)
        {
            const string defaultSource = "https://api.nuget.org/v3/index.json";
            NuGetSources[defaultSource] = new NuGetSource(defaultSource);
        }
    }

    private IDictionary<string, NuGetSource> NuGetSources { get; } =
        new Dictionary<string, NuGetSource>(StringComparer.OrdinalIgnoreCase);

    private IAppContext AppContext { get; }

    private NuGetConfig NuGetConfig { get; }

    private SourceCacheContext SourceCacheContext { get; }

    public async Task ClearNuGetHttpCacheAsync()
    {
        await DotnetNuGetTools.ClearNuGetHttpCacheAsync();
    }

    public async Task DownloadPackageAsync(
        string packageId,
        NuGetVersion nugetVersion,
        CancellationToken token)
    {
        var nugetSource = GetDefaultNuGetSource();

        var downloadResource =
            await nugetSource.SourceRepository.GetResourceAsync<DownloadResource>(token);

        var downloadResult = await downloadResource.GetDownloadResourceResultAsync(
            new PackageIdentity(packageId, nugetVersion),
            new PackageDownloadContext(SourceCacheContext),
            AppContext.GetTempPath(),
            NullLogger.Instance,
            token);

        if (downloadResult.Status != DownloadResourceResultStatus.Available)
        {
            throw new FileNotFoundException($"Download package <{packageId}> failed.");
        }

        downloadResult.PackageStream.CopyToFile(
            GetNuGetPackageCacheFilePath(packageId, nugetVersion.ToString()));
    }

    public string GetNuGetPackageCacheFilePath(string packageId, string nugetVersion)
    {
        return Path.Combine(
            GetCachePath(),
            $"{packageId}.{nugetVersion}.nupkg");
    }

    public async Task<IPackageSearchMetadata> GetPackageMetadataAsync(
        string packageId,
        NuGetVersion nugetVersion,
        CancellationToken token)
    {
        var nugetSource = GetDefaultNuGetSource();

        var packageMetadataResource =
            await nugetSource.SourceRepository.GetResourceAsync<PackageMetadataResource>(token);

        var metadata = await packageMetadataResource.GetMetadataAsync(
            new PackageIdentity(packageId, nugetVersion),
            SourceCacheContext,
            NullLogger.Instance,
            token);

        return metadata;
    }

    public string GetCachePath()
    {
        return Path.Combine(AppContext.GetMainAppDirectory(), NuGetConfig.CacheFolder);
    }

    public async Task DownloadPackageAndDependenciesRecursiveAsync(
        string packageId,
        string version,
        HashSet<string> processedPackages,
        CancellationToken token)
    {
        string ProcessPackage(string id, string v)
        {
            return $"{id}.{v}";
        }

        if (processedPackages.Contains(ProcessPackage(packageId, version)))
        {
            return;
        }

        var nugetVersion = new NuGetVersion(version);

        if (!File.Exists(GetNuGetPackageCacheFilePath(packageId, version)))
        {
            await DownloadPackageAsync(packageId, nugetVersion, token);
        }

        processedPackages.Add(ProcessPackage(packageId, version));

        var nugetSource = GetDefaultNuGetSource();

        var packageMetadataResource =
            await nugetSource.SourceRepository.GetResourceAsync<PackageMetadataResource>(token);

        var metadata = await packageMetadataResource.GetMetadataAsync(
            new PackageIdentity(packageId, nugetVersion),
            SourceCacheContext,
            NullLogger.Instance,
            token);

        foreach (var dependencyGroup in metadata.DependencySets)
        {
            // !WARNING dependencyGroup.TargetFramework
            if (dependencyGroup.TargetFramework.ToString() == ProductInfo.TargetFramework
                || dependencyGroup.TargetFramework.ToString().Contains(".NETStandard"))
            {
                foreach (var dependency in dependencyGroup.Packages)
                {
                    var dependencyVersion = dependency.VersionRange.MinVersion?.ToString();
                    if (dependencyVersion != null)
                    {
                        await DownloadPackageAndDependenciesRecursiveAsync(
                            dependency.Id,
                            dependencyVersion,
                            processedPackages,
                            token);
                    }
                }
            }
        }
    }

    public async Task<NuGetVersion> GetSearchMetadataAsync(
        string packageId,
        bool includePrerelease,
        CancellationToken token)
    {
        var nugetSource = GetDefaultNuGetSource();

        var metadataResource =
            await nugetSource.SourceRepository.GetResourceAsync<MetadataResource>(token);

        var searchMetadata = await metadataResource.GetLatestVersion(
            packageId,
            includePrerelease,
            false,
            SourceCacheContext,
            NullLogger.Instance,
            token);

        if (searchMetadata == null)
        {
            throw new FileNotFoundException("Can't find product from any configured source.");
        }

        return searchMetadata;
    }


    public async Task<string?> FetchReleaseNotesAsync(
        string packageId,
        string version)
    {
        using var httpClient = new HttpClient();

        foreach (var source in NuGetSources.Keys)
        {
            var patchNote = await TryFetchReleaseNotesAsync(
                httpClient,
                source,
                packageId,
                version);
            if (!string.IsNullOrWhiteSpace(patchNote))
            {
                return patchNote;
            }
        }

        return null;
    }

    private string GetDefaultSource()
    {
        if (NuGetConfig.Sources is { Length: > 0 })
        {
            foreach (var source in NuGetConfig.Sources)
            {
                if (!string.IsNullOrWhiteSpace(source))
                {
                    return source;
                }
            }
        }

        if (NuGetSources.Count > 0)
        {
            return NuGetSources.Keys.First();
        }

        throw new InvalidOperationException("No NuGet sources configured.");
    }

    private NuGetSource GetDefaultNuGetSource()
    {
        return GetNuGetSource(GetDefaultSource());
    }

    private NuGetSource GetNuGetSource(string source)
    {
        if (!NuGetSources.TryGetValue(source, out var nugetSource))
        {
            nugetSource = new NuGetSource(source);
            NuGetSources[source] = nugetSource;
        }

        return nugetSource;
    }

    private async Task<string?> TryFetchReleaseNotesAsync(
        HttpClient httpClient,
        string nugetSouce,
        string packageId,
        string version)
    {
        using var indexJson = JsonDocument.Parse(
            await httpClient.GetStringAsync(nugetSouce, CancellationToken.None));
        var baseAddress = indexJson.RootElement
            .GetProperty("resources")
            .EnumerateArray()
            .Select(r => new
            {
                Type = r.GetProperty("@type").GetString(),
                Id = r.GetProperty("@id").GetString()
            })
            .First(x => x.Type != null && x.Type.StartsWith("PackageBaseAddress", StringComparison.OrdinalIgnoreCase))
            .Id!;


        var lowerId = packageId.ToLowerInvariant();
        var nugetVersion = NuGetVersion.Parse(version);
        var lowerVersion = nugetVersion.ToNormalizedString().ToLowerInvariant();

        var nuspecUrl = $"{baseAddress.TrimEnd('/')}/{lowerId}/{lowerVersion}/{lowerId}.nuspec";

        var xml = await httpClient.GetStringAsync(nuspecUrl, CancellationToken.None);
        var xdoc = XDocument.Parse(xml);

        var releaseNotes = xdoc
            .Descendants()
            .FirstOrDefault(e => e.Name.LocalName.Equals("releaseNotes", StringComparison.OrdinalIgnoreCase))
            ?.Value.Trim();

        return string.IsNullOrWhiteSpace(releaseNotes) ? null : releaseNotes;
    }
}