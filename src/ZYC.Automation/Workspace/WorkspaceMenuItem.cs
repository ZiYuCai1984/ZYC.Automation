using System.Windows.Input;
using ZYC.Automation.Abstractions.Workspace;

namespace ZYC.Automation.Workspace;

internal class WorkspaceMenuItem : IWorkspaceMenuItem
{
    public WorkspaceMenuItem(string title, ICommand command, string icon, bool localization = true)
    {
        Title = title;
        Command = command;
        Icon = icon;
        Localization = localization;
    }

    public string Title { get; }

    public ICommand Command { get; }

    public string Icon { get; }

    public bool Localization { get; }
}