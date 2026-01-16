using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Config;
using ZYC.Automation.Abstractions.StatusBar;
using ZYC.Automation.Modules.Settings.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.StatusBar;

[RegisterSingleInstanceAs(typeof(StatusBarView), typeof(IStatusBarView))]
internal sealed partial class StatusBarView : INotifyPropertyChanged, IDisposable, IStatusBarView
{
    public StatusBarView(
        StatusBarConfig statusBarConfig,
        IEventAggregator eventAggregator,
        IStatusBarManager statusBarManager)
    {
        StatusBarConfig = statusBarConfig;
        EventAggregator = eventAggregator;
        StatusBarManager = statusBarManager;

        InitializeComponent();

        StatusBarConfigChangedEvent =
            EventAggregator.Subscribe<SettingChangedEvent<StatusBarConfig>>(OnStatusBarConfigChanged);
    }

    private IDisposable StatusBarConfigChangedEvent { get; }

    private StatusBarConfig StatusBarConfig { get; }

    private IEventAggregator EventAggregator { get; }

    private IStatusBarManager StatusBarManager { get; }

    public bool IsStatusBarVisible => StatusBarConfig.IsVisible;

    public IStatusBarItem[] LeftStatusBarItems => StatusBarManager.GetLeftItems();

    public IStatusBarItem[] RightStatusBarItems => StatusBarManager.GetRightItems();

    public void Dispose()
    {
        StatusBarConfigChangedEvent.Dispose();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public double GetActualHeight()
    {
        if (!StatusBarConfig.IsVisible)
        {
            return 0;
        }

        return Height;
    }

    private void OnStatusBarConfigChanged(SettingChangedEvent<StatusBarConfig> obj)
    {
        OnPropertyChanged(nameof(IsStatusBarVisible));
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}