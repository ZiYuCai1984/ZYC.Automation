using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.Commands;

[RegisterSingleInstance]
internal class RepairNuGetModuleCommand : AsyncCommandBase<INuGetModule>
{
}