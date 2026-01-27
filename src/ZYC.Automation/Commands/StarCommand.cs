using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Abstractions.QuickBar;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Commands;

[Register]
internal class StarCommand : CommandBase, IDisposable
{
    public StarCommand(
        ITabManager tabManager,
        WorkspaceNode workspaceNode,
        IEventAggregator eventAggregator,
        IStarQuickBarItemsProvider starQuickBarItemsProvider)
    {
        TabManager = tabManager;
        WorkspaceNode = workspaceNode;
        EventAggregator = eventAggregator;
        StarQuickBarItemsProvider = starQuickBarItemsProvider;

        EventAggregator.Subscribe<NavigateCompletedEvent>(OnNavigateCompleted)
            .DisposeWith(CompositeDisposable);
        EventAggregator.Subscribe<TabItemClosedEvent>(OnTabItemClosed)
            .DisposeWith(CompositeDisposable);
        EventAggregator.Subscribe<FocusedTabItemChangedEvent>(OnFocusedTabItemChanged)
            .DisposeWith(CompositeDisposable);
    }

    private CompositeDisposable CompositeDisposable { get; } = new();


    private ITabManager TabManager { get; }

    private WorkspaceNode WorkspaceNode { get; }

    private IEventAggregator EventAggregator { get; }

    private IStarQuickBarItemsProvider StarQuickBarItemsProvider { get; }

    public string Icon
    {
        get
        {
            if (IsStared())
            {
                return "Star";
            }

            return "StarOutline";
        }
    }

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }

    private void OnFocusedTabItemChanged(FocusedTabItemChangedEvent obj)
    {
        RaiseCanExecuteChanged();
    }

    private void OnTabItemClosed(TabItemClosedEvent obj)
    {
        RaiseCanExecuteChanged();
    }

    private void OnNavigateCompleted(NavigateCompletedEvent obj)
    {
        RaiseCanExecuteChanged();
    }

    protected override void InternalExecute(object? parameter)
    {
        var tabItemInstance = TabManager.GetFocusedTabItemInstance(WorkspaceNode.Id);
        if (tabItemInstance == null)
        {
            throw new InvalidOperationException();
        }

        var uri = tabItemInstance.Uri;
        var icon = tabItemInstance.Icon;

        if (IsStared())
        {
            UnStar(uri);
        }
        else
        {
            Star(uri, icon);
        }
    }

    private void UnStar(Uri uri)
    {
        StarQuickBarItemsProvider.DetachMenuItem(uri);
    }

    private void Star(Uri uri, string icon)
    {
        var menuItem = StarQuickBarItemsProvider.CreateQuickMenuItem(uri, icon);

        StarQuickBarItemsProvider.AttachItem(menuItem);
    }


    private bool IsStared()
    {
        var tabItemInstance = TabManager.GetFocusedTabItemInstance(WorkspaceNode.Id);
        if (tabItemInstance == null)
        {
            return false;
        }

        return StarQuickBarItemsProvider.CheckIsStared(tabItemInstance.Uri);
    }

    public override bool CanExecute(object? parameter)
    {
        OnPropertyChanged(nameof(Icon));

        var tabItemInstance = TabManager.GetFocusedTabItemInstance(WorkspaceNode.Id);
        if (tabItemInstance == null)
        {
            return false;
        }

        return true;
    }
}