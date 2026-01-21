using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.NuGet.Abstractions;
using ZYC.Automation.Modules.NuGet.Abstractions.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.NuGet.Commands;

[RegisterSingleInstanceAs(typeof(IClearNuGetHttpCacheCommand))]
internal class ClearNuGetHttpCacheCommand : AsyncCommandBase, IClearNuGetHttpCacheCommand
{
    public ClearNuGetHttpCacheCommand(
        INuGetManager nuGetManager, 
        IAppLogger<ClearNuGetHttpCacheCommand> logger)
    {
        NuGetManager = nuGetManager;
        Logger = logger;
    }

    private INuGetManager NuGetManager { get; }
    private IAppLogger<ClearNuGetHttpCacheCommand> Logger { get; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        try
        {
            await NuGetManager.ClearNuGetHttpCacheAsync();
        }
        catch (Exception e)
        {
            Logger.Error(e);
        }
    }
}