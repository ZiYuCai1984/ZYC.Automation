using Autofac;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.Secrets.Abstractions;
using ZYC.Automation.Modules.Settings.Abstractions;
using ZYC.CoreToolkit.Abstractions.Settings;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.Secrets;

internal class Module : ModuleBase
{
    public override string Icon => SecretsTabItem.Constants.Icon;

    public override Task RegisterAsync(ContainerBuilder builder)
    {
        builder.RegisterAdapter<IConfig[], ISecrets[]>(configs =>
            configs.OfType<ISecrets>().ToArray());

        return base.RegisterAsync(builder);
    }

    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<SecretsTabItemFactory>();

        var settingsMainMenuItem = lifetimeScope.Resolve<ISettingsMainMenuItemsProvider>();
        settingsMainMenuItem.RegisterSubItem<SecretMainMenuItem>();


        lifetimeScope.RegisterTabItemFactory<PasswordGeneratorTabItemFactory>();
        lifetimeScope.Resolve<IToolsMainMenuItemsProvider>()
            .RegisterSubItem<PasswordGeneratorMainMenuItem>();

        lifetimeScope.RegisterTabItemFactory<WlanPasswordTabItemFactory>();
        lifetimeScope.Resolve<IToolsMainMenuItemsProvider>().RegisterSubItem<WlanPasswordMainMenuItem>();


        return Task.CompletedTask;
    }
}