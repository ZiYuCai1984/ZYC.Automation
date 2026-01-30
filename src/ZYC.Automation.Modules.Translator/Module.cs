using Autofac;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Translator.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.Translator;

internal class Module : ModuleBase
{
    public override string Icon => TranslatorModuleConstants.Icon;


    public override async Task LoadAsync(ILifetimeScope lifetimeScope)
    {
        await Task.CompletedTask;

        if (lifetimeScope.TryResolve<ICommandlineResourcesProvider>(out var commandlineResourcesProvider))
        {
            commandlineResourcesProvider.Register(
                new CommandlineServiceOptions
                {
                    Name = "libretranslate",
                    Command = "libretranslate --load-only en,ja,zh,zt,ko"
                });
        }
    }
}