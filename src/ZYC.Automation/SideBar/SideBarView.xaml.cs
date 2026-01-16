using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Abstractions.SideBar;
using ZYC.Automation.Modules.Settings.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.SideBar;

[RegisterSingleInstance]
internal sealed partial class SideBarView : INotifyPropertyChanged
{
    public SideBarView(
        ISideBarManager sideBarManager,
        IEventAggregator eventAggregator,
        SideBarConfig sideBarConfig)
    {
        SideBarManager = sideBarManager;
        EventAggregator = eventAggregator;
        SideBarConfig = sideBarConfig;

        InitializeComponent();

        ConfigChangedEvent = EventAggregator.Subscribe<SettingChangedEvent<SideBarConfig>>(OnConfigChanged);
    }

    private IDisposable ConfigChangedEvent { get; }

    public bool IsSideBarVisible => SideBarConfig.IsVisible;

    private ISideBarManager SideBarManager { get; }

    private IEventAggregator EventAggregator { get; }

    private SideBarConfig SideBarConfig { get; }

    private bool FirstRending { get; set; } = true;

    public ObservableCollection<ISideBarItem> TopSideBarItems { get; } = new();

    public ObservableCollection<ISideBarItem> BottomSideBarItems { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnConfigChanged(SettingChangedEvent<SideBarConfig> e)
    {
        var oldValue = e.OldValue;
        var newValue = e.NewValue;

        if (oldValue.IsVisible == newValue.IsVisible)
        {
            return;
        }

        OnPropertyChanged(nameof(IsSideBarVisible));
    }

    private void OnSlideBarLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstRending)
        {
            return;
        }

        FirstRending = false;

        var topItems = SideBarManager.GetTopItems();
        foreach (var item in topItems)
        {
            TopSideBarItems.Add(item);
        }

        var bottomItems = SideBarManager.GetBottomItems();
        foreach (var item in bottomItems)
        {
            BottomSideBarItems.Add(item);
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}