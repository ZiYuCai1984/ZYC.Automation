using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Language.Abstractions;
using ZYC.Automation.Modules.Language.UI;
using ZYC.Automation.Modules.Translator.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Language;

[Register]
internal class LanguageTabItem : TabItemInstanceBase<LanguageView>
{
    public LanguageTabItem(
        ILanguageManager languageManager,
        ILifetimeScope lifetimeScope) : base(lifetimeScope)
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

            return Constants.DefaultIcon;
        }
    }

    public static class Constants
    {
        public static string DefaultIcon => "FormatTextVariantOutline";

        public static string Host => "lang";

        public static string Title => "Language";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}