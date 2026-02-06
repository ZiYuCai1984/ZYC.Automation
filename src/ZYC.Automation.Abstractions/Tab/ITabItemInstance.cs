namespace ZYC.Automation.Abstractions.Tab;

public interface ITabItemInstance : IDisposable
{
    TabReference TabReference { get; }

    string Title { get; }

    string Icon { get; }

    object View { get; }

    bool Localization { get; }

    Task LoadAsync();

    bool OnClosing();
}

public static class ITabItemInstanceEx
{
    extension(ITabItemInstance instance)
    {
        public Guid Id => instance.TabReference.Id;

        public Uri Uri => instance.TabReference.Uri;

        public string Scheme => instance.Uri.Scheme;

        public string Host => instance.Uri.Host;

    }
}