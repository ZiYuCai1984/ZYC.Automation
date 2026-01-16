using System.Diagnostics;
using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class OpenGitHubCommand : CommandBase
{
    protected override void InternalExecute(object? parameter)
    {
        Process.Start(new ProcessStartInfo(ProductInfo.Repository) { UseShellExecute = true });
    }
}