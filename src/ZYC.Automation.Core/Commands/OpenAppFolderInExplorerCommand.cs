using System.Diagnostics;
using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class OpenAppFolderInExplorerCommand : CommandBase
{
    public OpenAppFolderInExplorerCommand(IAppContext appContext)
    {
        AppContext = appContext;
    }

    private IAppContext AppContext { get; }

    protected override void InternalExecute(object? parameter)
    {
        var dir = AppContext.GetMainAppDirectory();
        Process.Start("explorer.exe", dir);
    }
}