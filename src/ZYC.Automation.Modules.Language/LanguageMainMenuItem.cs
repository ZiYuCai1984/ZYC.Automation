using System.ComponentModel;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Runtime.CompilerServices;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.MainMenu;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Core.Menu;
using ZYC.Automation.Modules.Language.Abstractions;
using ZYC.Automation.Modules.Language.Abstractions.Event;
using ZYC.Automation.Modules.Translator.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Language;

[RegisterSingleInstance]
internal class LanguageMainMenuItem : MainMenuItemsProvider
{
    public LanguageMainMenuItem(
        ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
        Info = new MenuItemInfo
        {
            Title = LanguageTabItem.Constants.Title,
            Icon = LanguageTabItem.Constants.DefaultIcon
        };

        var languages = Enum.GetValues<LanguageType>();
        foreach (var language in languages)
        {
            RegisterSubItem(
                LifetimeScope.Resolve<SetLanguageOptionMainMenuItem>(new TypedParameter(typeof(LanguageType),
                    language)));
        }

        //RegisterSubItem(new MainMenuItem("Detail",
        //    "ListBoxOutline",
        //    lifetimeScope.CreateNavigateCommand(LanguageTabItem.Constants.Uri)));
    }

    public override MenuItemInfo Info { get; }
}

[Register]
internal class SetLanguageOptionMainMenuItem : MainMenuItem, IDisposable, INotifyPropertyChanged
{
    public SetLanguageOptionMainMenuItem(
        IEventAggregator eventAggregator,
        LanguageType languageType,
        ILanguageManager languageManager,
        LanguageConfig languageConfig)
    {
        TargetLanguageType = languageType;
        LanguageConfig = languageConfig;

        Info = new MenuItemInfo
        {
            Title = languageType.ToDisplayName(),
            Icon = Base64IconResources.Empty1x1,
            Localization = false
        };

        Command = new RelayCommand(_ => languageManager.GetCurrentLanguageType() != TargetLanguageType, _ =>
        {
            languageManager.SetCurrentLanguageType(TargetLanguageType);
        });

        eventAggregator.Subscribe<LanguageChangedEvent>(OnLanguageChanged)
            .DisposeWith(CompositeDisposable);
    }


    private CompositeDisposable CompositeDisposable { get; } = new();

    private LanguageType TargetLanguageType { get; }

    private LanguageConfig LanguageConfig { get; }

    public override string Title
    {
        get
        {
            if (LanguageConfig.CurrentLanguage != TargetLanguageType)
            {
                return Info.Title;
            }

            return $"{Info.Title} ✔️";
        }
    }

    public void Dispose()
    {
        CompositeDisposable.Dispose();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnLanguageChanged(LanguageChangedEvent obj)
    {
        OnPropertyChanged(nameof(Title));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}