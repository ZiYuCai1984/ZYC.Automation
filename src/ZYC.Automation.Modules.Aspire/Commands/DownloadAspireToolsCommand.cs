using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.Commands;

[RegisterSingleInstance]
internal class DownloadAspireToolsCommand : AsyncCommandBase
{
    public DownloadAspireToolsCommand(IAspireServiceManager aspireServiceManager)
    {
        AspireServiceManager = aspireServiceManager;
    }

    private IAspireServiceManager AspireServiceManager { get; }

    protected override Task InternalExecuteAsync(object? parameter)
    {
        return AspireServiceManager.DownloadAspireToolsAsync();
    }
}