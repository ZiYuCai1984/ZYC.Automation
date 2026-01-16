namespace ZYC.Automation.Abstractions.QuickBar;

public interface IStarQuickBarItemsProvider : IQuickBarItemsProvider
{
    bool CheckIsStared(Uri uri);

    void DetachMenuItem(Uri uri);

    StarQuickBarItem CreateQuickMenuItem(Uri uri, string icon);
}