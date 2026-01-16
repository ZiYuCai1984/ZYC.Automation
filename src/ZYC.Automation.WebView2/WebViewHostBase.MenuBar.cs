using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.WebView2.Commands;
using Debugger = System.Diagnostics.Debugger;

namespace ZYC.Automation.WebView2;

public partial class WebViewHostBase
{
    private CommandBase? _goBackCommand;

    private CommandBase? _goForwardCommand;

    private CommandBase? _homePageCommand;

    private CommandBase? _openDevToolsWindowCommand;

    private CommandBase? _refreshCommand;

    public virtual string? HomePageUri { get; protected set; }

    public virtual CommandBase? GoBackCommand
    {
        get
        {
            if (_goBackCommand == null)
            {
                var command = LifetimeScope.Resolve<GoBackCommand>();
                command.SetView(this);
                _goBackCommand = command;
            }

            return _goBackCommand;
        }
    }

    public virtual CommandBase? GoForwardCommand
    {
        get
        {
            if (_goForwardCommand == null)
            {
                var command = LifetimeScope.Resolve<GoForwardCommand>();
                command.SetView(this);
                _goForwardCommand = command;
            }

            return _goForwardCommand;
        }
    }

    public virtual CommandBase? RefreshCommand
    {
        get
        {
            if (_refreshCommand == null)
            {
                var command = LifetimeScope.Resolve<RefreshCommand>();
                command.SetView(this);
                _refreshCommand = command;
            }

            return _refreshCommand;
        }
    }

    public virtual CommandBase? OpenDevToolsWindowCommand
    {
        get
        {
            if (_openDevToolsWindowCommand == null)
            {
                var command = LifetimeScope.Resolve<OpenDevToolsWindowCommand>();
                command.SetView(this);
                _openDevToolsWindowCommand = command;
            }

            return _openDevToolsWindowCommand;
        }
    }

    public virtual CommandBase? HomePageCommand
    {
        get
        {
            if (_homePageCommand == null)
            {
                var command = LifetimeScope.Resolve<HomePageCommand>();
                command.SetView(this);
                _homePageCommand = command;
            }

            return _homePageCommand;
        }
    }

    private bool IsMenuBarSetuped { get; set; }

    public virtual bool IsMenuBarVisible { get; private set; } = true;

    internal void RaiseMenuBarCommandsCanExecuteChanged()
    {
        GoBackCommand?.RaiseCanExecuteChanged();
        GoForwardCommand?.RaiseCanExecuteChanged();
        RefreshCommand?.RaiseCanExecuteChanged();
        OpenDevToolsWindowCommand?.RaiseCanExecuteChanged();
        HomePageCommand?.RaiseCanExecuteChanged();
    }

    protected void SetupMenuBar()
    {
        if (IsMenuBarSetuped)
        {
            Debugger.Break();
            return;
        }

        IsMenuBarSetuped = true;

        var menuBarView = LifetimeScope.Resolve<MenuBarView>();
        menuBarView.DataContext = this;

        MainGrid.Children.Add(menuBarView);
    }

    public bool CanGoBack()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (CoreWebView2 == null)
        {
            return false;
        }

        return CoreWebView2.CanGoBack;
    }

    public bool CanGoForward()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (CoreWebView2 == null)
        {
            return false;
        }

        return CoreWebView2.CanGoForward;
    }

    public void GoBack()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        CoreWebView2?.GoBack();
    }

    public void GoForward()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        CoreWebView2?.GoForward();
    }
}