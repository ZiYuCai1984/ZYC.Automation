using ZYC.Automation.Abstractions.Config.Attributes;
using ZYC.Automation.Modules.Translator.Abstractions;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.Language.Abstractions;

/// <summary>
///     Defines configuration for language resources.
/// </summary>
[Hidden]
public interface ILanguageResourcesConfig : IConfig
{
    /// <summary>
    ///     Gets or sets the language resources keyed by language type.
    /// </summary>
    public Dictionary<LanguageType, Dictionary<string, string>> Resources { get; set; }
}