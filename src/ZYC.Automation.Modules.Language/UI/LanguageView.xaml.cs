using ZYC.Automation.Modules.Language.Abstractions;
using ZYC.Automation.Modules.Translator.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Language.UI;

[Register]
internal partial class LanguageView
{
    private LanguageType _languageType;

    public LanguageView(ILanguageManager languageManager)
    {
        LanguageManager = languageManager;

        _languageType = languageManager.GetCurrentLanguageType();
    }

    public LanguageType LanguageType
    {
        get => _languageType;
        set
        {
            if (_languageType == value)
            {
                return;
            }

            _languageType = value;
            LanguageManager.SetCurrentLanguageType(_languageType);
        }
    }

    private ILanguageManager LanguageManager { get; }
}