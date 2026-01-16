namespace ZYC.Automation.Abstractions.Tab;

public interface ITabItemInstance : IDisposable
{
    TabReference TabReference { get; }

    Guid Id { get; }

    Uri Uri { get; }

    string Scheme { get; }

    string Host { get; }

    string Title { get; }

    string Icon { get; }

    object View { get; }

    bool Localization { get; }

    Task LoadAsync();

    bool OnClosing();
}