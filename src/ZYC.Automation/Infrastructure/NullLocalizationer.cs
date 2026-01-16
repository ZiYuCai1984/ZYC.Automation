using ZYC.Automation.Core.Localizations;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Infrastructure;

[RegisterSingleInstanceAs(typeof(ILocalizationer))]
internal class NullLocalizationer : ILocalizationer
{
    public string Localization(string text)
    {
        return text;
    }

    public Task<string> LocalizationAsync(string text)
    {
        return Task.FromResult(text);
    }
}