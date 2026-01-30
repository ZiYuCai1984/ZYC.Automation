namespace ZYC.Automation.Abstractions.Tab;

public interface ITabItemInstance : IDisposable
{
    TabReference TabReference { get; }

    Guid Id => TabReference.Id;

    Uri Uri => TabReference.Uri;

    string Scheme => Uri.Scheme;

    string Host => Uri.Host;

    string Title { get; }

    string Icon { get; }

    object View { get; }

    bool Localization { get; }

    Task LoadAsync();

    bool OnClosing();
}