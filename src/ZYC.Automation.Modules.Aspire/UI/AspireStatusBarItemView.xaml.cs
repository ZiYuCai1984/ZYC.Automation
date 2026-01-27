using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Runtime.CompilerServices;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.UI;

[RegisterSingleInstance]
internal partial class AspireStatusBarItemView : IDisposable, INotifyPropertyChanged
{
    public AspireStatusBarItemView(
        IEventAggregator eventAggregator,
        IAspireMainMenuItemsProvider aspireMainMenuItemsProvider,
        IAspireServiceManager aspireServiceManager)
    {
        AspireServiceManager = aspireServiceManager;
        MainMenuItems = aspireMainMenuItemsProvider.SubItems;

        eventAggregator.Subscribe<AspireServiceStatusChangedEvent>(OnAspireServiceStatusChanged)
            .DisposeWith(CompositeDisposable);

        InitializeComponent();
    }

    private CompositeDisposable CompositeDisposable { get; } = new();

    private IAspireServiceManager AspireServiceManager { get; }

    public static string Icon => AspireTabItem.Constants.Icon;

    public IMainMenuItem[] MainMenuItems { get; }

    public AspireServiceStatus AspireServiceStatus => AspireServiceManager.GetStatusSnapshot();

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnAspireServiceStatusChanged(AspireServiceStatusChangedEvent obj)
    {
        OnPropertyChanged(nameof(AspireServiceStatus));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}