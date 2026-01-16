using System.Collections.ObjectModel;
using System.Windows;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Abstractions.Workspace;
using ZYC.CoreToolkit.Abstractions.Attributes;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.SideBar;

[RegisterSingleInstance]
[TempCode]
[Obsolete]
internal partial class SideMenuView
{
    public SideMenuView(
        IParallelWorkspaceManager parallelWorkspaceManager,
        IMainMenuManager mainMenuManager)
    {
        ParallelWorkspaceManager = parallelWorkspaceManager;
        MainMenuManager = mainMenuManager;

        InitializeComponent();
    }

    private IParallelWorkspaceManager ParallelWorkspaceManager { get; }
    private IMainMenuManager MainMenuManager { get; }

    private bool FirstRending { get; set; } = true;

    public ObservableCollection<IMainMenuItem?> MainMenuItems { get; } = new();

    private void OnMenuLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstRending)
        {
            return;
        }

        FirstRending = false;

        var items = MainMenuManager.GetSortedItems();
        foreach (var item in items)
        {
            MainMenuItems.Add(item);
        }
    }
}