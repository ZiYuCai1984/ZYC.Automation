using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac;

namespace ZYC.Automation.Modules.NuGet;

internal class Module : ModuleBase
{
    public override string Icon => Base64IconResources.NuGet;
}