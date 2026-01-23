using System.IO;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.CLI;

internal class Module : ModuleBase
{
    public override string Icon => CLITabItem.Constants.Icon;

    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<CLITabItemFactory>();
        lifetimeScope.RegisterRootMainMenuItem<CLIMainMenuItem>();


        var appContext = lifetimeScope.Resolve<IAppContext>();


        NativeDllLoaderTools.LoadFrom(
            Path.Combine(
                appContext.GetMainAppDirectory(),
                "runtimes",
                "win10-x64",
                "native",
                "conpty.dll"));

        NativeDllLoaderTools.LoadFrom(
            Path.Combine(
                appContext.GetMainAppDirectory(),
                "runtimes",
                "win-x64",
                "native",
                "Microsoft.Terminal.Control.dll"));

        return Task.CompletedTask;
    }
}