using Autofac;
using ZYC.Automation.Core;
using ZYC.Automation.Modules.Language.Abstractions;
using ZYC.Automation.Modules.Settings.Abstractions;
using ZYC.CoreToolkit.Abstractions.Settings;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.Language;

internal class Module : ModuleBase
{
    public override async Task RegisterAsync(ContainerBuilder builder)
    {
        await Task.CompletedTask;

        builder.RegisterAdapter<IConfig[], ILanguageResourcesConfig[]>(configs =>
            configs.OfType<ILanguageResourcesConfig>().ToArray());
    }

    public override Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        lifetimeScope.RegisterTabItemFactory<LanguageTabItemFactory>();

        var settingsMainMenuItem = lifetimeScope.Resolve<ISettingsMainMenuItemsProvider>();
        settingsMainMenuItem.RegisterSubItem<LanguageMainMenuItem>();

        return Task.CompletedTask;
    }
}