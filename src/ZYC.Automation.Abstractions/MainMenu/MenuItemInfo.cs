namespace ZYC.Automation.Abstractions.MainMenu;

public class MenuItemInfo
{
    public string Title { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public string Anchor { get; set; } = MainMenuAnchors.Default;

    public int Priority { get; set; }

    public bool Localization { get; set; } = true;
}