using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Language.Abstractions;
using ZYC.Automation.Modules.Language.UI;
using ZYC.Automation.Modules.Translator.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Language;

[Register]
[ConstantsSource(typeof(LanguageModuleConstants))]
internal class LanguageTabItem : TabItemInstanceBase<LanguageView>
{
    public LanguageTabItem(
        ILanguageManager languageManager,
        ILifetimeScope lifetimeScope, 
        TabReference tabReference) : base(lifetimeScope, tabReference)
    {
        LanguageManager = languageManager;
    }

    private ILanguageManager LanguageManager { get; }

    public override string Icon
    {
        get
        {
            if (LanguageManager.GetCurrentLanguageType() == LanguageType.ja)
            {
                return "SyllabaryHiragana";
            }

            return LanguageModuleConstants.DefaultIcon;
        }
    }
}