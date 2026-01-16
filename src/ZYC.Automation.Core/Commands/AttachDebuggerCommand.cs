using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class AttachDebuggerCommand : CommandBase
{
    protected override void InternalExecute(object? parameter)
    {
        DebuggerTools.Break();
    }
}