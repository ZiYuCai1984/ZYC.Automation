using ZYC.Automation.Abstractions.Config.Attributes;
using ZYC.CoreToolkit.Abstractions.Settings;

namespace ZYC.Automation.Modules.Secrets.Abstractions;

/// <summary>
///     Marks a configuration object as a secrets configuration.
/// </summary>
[Hidden]
public interface ISecrets : IConfig
{
}