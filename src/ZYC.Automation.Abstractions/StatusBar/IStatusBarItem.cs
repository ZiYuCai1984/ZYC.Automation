namespace ZYC.Automation.Abstractions.StatusBar;

public interface IStatusBarItem
{
    object View { get; }

    StatusBarSection Section { get; }

    int Order => 0;
}