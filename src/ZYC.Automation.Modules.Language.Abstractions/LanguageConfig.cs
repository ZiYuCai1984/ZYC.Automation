using ZYC.Automation.Abstractions.Config.Attributes;
using ZYC.Automation.Modules.Translator.Abstractions;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.Language.Abstractions;

/// <summary>
/// Configuration options for language selection.
/// </summary>
[Hidden]
public class LanguageConfig : IConfig
{
    /// <summary>
    ///     Gets or sets the current language.
    /// </summary>
    public LanguageType CurrentLanguage { get; set; } = LanguageType.en;
}