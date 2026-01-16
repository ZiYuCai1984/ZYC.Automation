using System.ComponentModel;
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

        AspireServiceStatusChangedEvent =
            eventAggregator.Subscribe<AspireServiceStatusChangedEvent>(OnAspireServiceStatusChanged);

        InitializeComponent();
    }

    private IAspireServiceManager AspireServiceManager { get; }

    public static string Icon => AspireTabItem.Constants.Icon;

    public IMainMenuItem[] MainMenuItems { get; }

    private IDisposable AspireServiceStatusChangedEvent { get; }

    public AspireServiceStatus AspireServiceStatus => AspireServiceManager.GetStatusSnapshot();

    public void Dispose()
    {
        AspireServiceStatusChangedEvent.Dispose();
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