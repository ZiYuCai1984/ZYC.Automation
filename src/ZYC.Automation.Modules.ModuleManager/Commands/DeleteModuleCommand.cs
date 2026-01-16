using System.Text;
using ZYC.Automation.Core;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.ModuleManager.Abstractions;
using ZYC.CoreToolkit.Abstractions.Autofac;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager.Commands;

[RegisterSingleInstance]
internal class DeleteModuleCommand : CommandBase
{
    public DeleteModuleCommand(
        ILocalModuleManager localModuleManager)
    {
        LocalModuleManager = localModuleManager;
    }

    private ILocalModuleManager LocalModuleManager { get; }

    public override bool CanExecute(object? parameter)
    {
        var module = (IModuleInfo)parameter!;
        return !LocalModuleManager.IsMoudlePendingDelete(module);
    }

    protected override void InternalExecute(object? parameter)
    {
        if (parameter == null)
        {
            return;
        }

        var module = (IModuleInfo)parameter;


        var warningPrompt = new StringBuilder();
        warningPrompt.AppendLine("This action cannot be undone !!");


        var dependencyBy = LocalModuleManager.GetDependencyBy(module);

        if (dependencyBy.Length != 0)
        {
            warningPrompt.AppendLine("The following dependencies will also be automatically deleted.");
            warningPrompt.AppendLine();

            foreach (var m in dependencyBy)
            {
                warningPrompt.AppendLine($"- {m.ModuleAssemblyName}");
            }
        }


        if (!MessageBoxTools.Confirm(warningPrompt.ToString(), "Warning", false))
        {
            return;
        }

        LocalModuleManager.DeleteModuleRecursive(module);

        RaiseCanExecuteChanged();
    }
}