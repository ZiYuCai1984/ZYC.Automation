using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Core.Menu;
using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.Automation.Modules.Aspire.Abstractions.Event;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire;

[RegisterSingleInstance]
internal class SetAspireBinarySourceMainMenuItem : MainMenuItemsProvider
{
    public SetAspireBinarySourceMainMenuItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = "Set Aspire Binary Source",
            Icon = "TuneVariant"
        };

        var sources = Enum.GetValues<AspireBinarySource>();
        foreach (var s in sources)
        {
            RegisterSubItem(lifetimeScope.Resolve<SetAspireBinarySourceOptionMainMenuItem>(
                new TypedParameter(typeof(AspireBinarySource), s)));
        }
    }

    public override MenuItemInfo Info { get; }
}

[Register]
internal class SetAspireBinarySourceOptionMainMenuItem : MainMenuItem, INotifyPropertyChanged, IDisposable
{
    public SetAspireBinarySourceOptionMainMenuItem(
        IAspireServiceManager aspireServiceManager,
        AspireBinarySource aspireBinarySource,
        AspireConfig aspireConfig,
        IEventAggregator eventAggregator)
    {
        Info = new MenuItemInfo
        {
            Title = aspireBinarySource.ToString(),
            Icon = "SourceCommit"
        };

        AspireServiceManager = aspireServiceManager;
        TargetAspireBinarySource = aspireBinarySource;
        AspireConfig = aspireConfig;
        AspireBinarySourceChangedEvent =
            eventAggregator.Subscribe<AspireConfigChangedEvent>(OnAspireConfigChanged);


        Command = new RelayCommand(_ => TargetAspireBinarySource != AspireConfig.AspireBinarySource,
            _ =>
            {
                AspireServiceManager.SetAspireBinarySource(TargetAspireBinarySource);
            }
        );
    }

    private IDisposable AspireBinarySourceChangedEvent { get; }

    private IAspireServiceManager AspireServiceManager { get; }

    private AspireBinarySource TargetAspireBinarySource { get; }

    private AspireConfig AspireConfig { get; }

    public override string Title
    {
        get
        {
            if (AspireConfig.AspireBinarySource != TargetAspireBinarySource)
            {
                return Info.Title;
            }

            return $"{Info.Title} ✔️";
        }
    }

    public void Dispose()
    {
        AspireBinarySourceChangedEvent.Dispose();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnAspireConfigChanged(AspireConfigChangedEvent obj)
    {
        OnPropertyChanged(nameof(Title));
    }


    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}