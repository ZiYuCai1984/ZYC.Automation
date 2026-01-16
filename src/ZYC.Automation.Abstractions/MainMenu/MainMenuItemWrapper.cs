using System.Windows.Input;

namespace ZYC.Automation.Abstractions.MainMenu;

public class MainMenuItemWrapper : IMainMenuItem
{
    private readonly IMainMenuItem _original;

    public MainMenuItemWrapper(IMainMenuItem original, IMainMenuItem[] sortedSubItems)
    {
        _original = original;
        SubItems = sortedSubItems;
    }

    public ICommand Command => _original.Command;
    public string Title => _original.Title;
    public string Icon => _original.Icon;
    public string Anchor => _original.Anchor;
    public int Priority => _original.Priority;
    public bool Localization => _original.Localization;

    public IMainMenuItem[] SubItems { get; }
}