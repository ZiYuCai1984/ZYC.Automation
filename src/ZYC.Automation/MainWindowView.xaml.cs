using System.Windows;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Core;
using ZYC.Automation.Taskbar;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation;

[RegisterSingleInstanceAs(typeof(MainWindowView), typeof(IRootGrid))]
internal partial class MainWindowView : IRootGrid
{
    public MainWindowView(
        ILifetimeScope lifetimeScope,
        TaskbarContextMenu taskbarContextMenu,
        AppConfig appConfig)
    {
        LifetimeScope = lifetimeScope;
        TaskbarContextMenu = taskbarContextMenu;
        AppConfig = appConfig;

        InitializeComponent();
    }

    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    private ILifetimeScope LifetimeScope { get; }
    private TaskbarContextMenu TaskbarContextMenu { get; }

    public AppConfig AppConfig { get; }

    private bool FirstRending { get; set; } = true;

    public object GetRootGrid()
    {
        return RootGrid;
    }


    private void OnMainWindowViewLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstRending)
        {
            return;
        }

        FirstRending = false;

        LifetimeScope.PublishEvent(new MainWindowLoadedEvent());
    }
}