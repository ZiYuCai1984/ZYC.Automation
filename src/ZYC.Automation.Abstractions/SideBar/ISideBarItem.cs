namespace ZYC.Automation.Abstractions.SideBar;

public interface ISideBarItem
{
    object View { get; }

    SideBarSection Section { get; }

    int Order => 0;
}