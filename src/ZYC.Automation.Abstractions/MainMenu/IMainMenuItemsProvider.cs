namespace ZYC.Automation.Abstractions.MainMenu;

public interface IMainMenuItemsProvider : IMainMenuItem
{
    void RegisterSubItem<T>() where T : IMainMenuItem;
}