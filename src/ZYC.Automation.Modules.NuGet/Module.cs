using ZYC.Automation.Modules.NuGet.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.NuGet;

internal class Module : ModuleBase
{
    public override string Icon => NuGetModuleConstants.Icon;
}