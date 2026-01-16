using System.Windows.Input;

namespace ZYC.Automation.Modules.Update.Abstractions.Commands;

public interface ICheckUpdateCommand : ICommand
{
    void Execute()
    {
        Execute(null);
    }
}