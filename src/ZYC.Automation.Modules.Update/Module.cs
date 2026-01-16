using Autofac;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.Automation.Modules.Update.Abstractions.Commands;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.Update;

internal class Module : ModuleBase
{
    private IDisposable? MainWindowLoadedEvent { get; set; }

    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<UpdateTabItemFactory>();
        lifetimeScope.RegisterRootMainMenuItem<UpdateMainMenuItem>();

        return Task.CompletedTask;
    }

    public override async Task AfterLoadedAsync(ILifetimeScope lifetimeScope)
    {
        await Task.CompletedTask;

        MainWindowLoadedEvent = lifetimeScope.SubscribeEvent<MainWindowLoadedEvent>(_ =>
        {
            var updateConfig = lifetimeScope.Resolve<UpdateConfig>();
            if (!updateConfig.CheckAtStartup)
            {
                return;
            }

            var checkUpdateCommand = lifetimeScope.Resolve<ICheckUpdateCommand>();
            checkUpdateCommand.Execute();
        });
    }
}