using Autofac;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Mock.Abstractions;
using ZYC.Automation.Modules.Mock.UI;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.Mock;

internal class Module : ModuleBase
{
    public override string Icon => "🚀";

    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<IMockTabItemFactory>();

        lifetimeScope.RegisterRootMainMenuItem<IMockMainMenuItemsProvider>();

        var mockTabItemFactory = lifetimeScope.Resolve<IMockTabItemFactory>();


        mockTabItemFactory.RegisterMockTabItem(
            new MockTabItemInfo(typeof(NotRegisteredExceptionView)));
        mockTabItemFactory.RegisterMockTabItem(
            new MockTabItemInfo(typeof(TestWorkspaceView)));
        mockTabItemFactory.RegisterMockTabItem(
            new MockTabItemInfo(typeof(TestNotificationView)));
        mockTabItemFactory.RegisterMockTabItem(
            new MockTabItemInfo(typeof(TestTaskManagerView)));
        mockTabItemFactory.RegisterMockTabItem(
            new MockTabItemInfo(typeof(TestCLIView)));

        if (lifetimeScope.TryResolve<ICommandlineResourcesProvider>(
                out var commandlineResourcesProvider))
        {
            commandlineResourcesProvider.Register(new CommandlineServiceOptions
            {
                Name = "mock",
                Command = "echo Hello World"
            });
        }

        return Task.CompletedTask;
    }
}