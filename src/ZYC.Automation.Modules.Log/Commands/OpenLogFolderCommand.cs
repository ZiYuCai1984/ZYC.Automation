using System.IO;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Log.Commands;

[RegisterSingleInstance]
internal class OpenLogFolderCommand : CommandBase
{
    public OpenLogFolderCommand(IAppContext appContext, ITabManager tabManager)
    {
        AppContext = appContext;
        TabManager = tabManager;
    }

    private IAppContext AppContext { get; }
    private ITabManager TabManager { get; }

    protected override void InternalExecute(object? parameter)
    {
        var dir = Path.Combine(AppContext.GetMainAppDirectory(), "logs");
        TabManager.NavigateAsync($"file:///{dir}");
    }
}