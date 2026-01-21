using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.ModuleManager.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.Commands;

[RegisterSingleInstance]
internal class RefreshNuGetModuleCommand : AsyncCommandBase<NuGetModuleManagerView>
{
    protected override async Task InternalExecuteAsync(NuGetModuleManagerView view)
    {
        await view.RefreshNuGetModulesAsync();
    }
}