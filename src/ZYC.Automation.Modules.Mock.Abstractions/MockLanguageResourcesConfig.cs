using ZYC.Automation.Modules.Language.Abstractions;
using ZYC.Automation.Modules.Translator.Abstractions;

namespace ZYC.Automation.Modules.Mock.Abstractions;

/// <summary>
///     Provides mock language resources for testing translations.
/// </summary>
public class MockLanguageResourcesConfig : ILanguageResourcesConfig
{
    /// <summary>
    ///     Gets or sets the language resources keyed by language and resource key.
    /// </summary>
    public Dictionary<LanguageType, Dictionary<string, string>> Resources { get; set; } = new();
}