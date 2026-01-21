using System.Windows.Input;

namespace ZYC.Automation.Modules.NuGet.Abstractions.Commands;

public interface IClearNuGetHttpCacheCommand : ICommand
{
    void Execute()
    {
        Execute(null);
    }
}