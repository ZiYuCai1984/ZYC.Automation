using System.Windows.Input;

namespace ZYC.Automation.Abstractions.Workspace;

public interface IWorkspaceMenuItem
{
    string Title { get; }

    ICommand Command { get; }

    string Icon { get; }

    bool Localization { get; }
}