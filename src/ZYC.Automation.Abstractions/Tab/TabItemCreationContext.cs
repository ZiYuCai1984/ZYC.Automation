namespace ZYC.Automation.Abstractions.Tab;

public class TabItemCreationContext
{
    public TabItemCreationContext(Uri uri, object lifetimeScope)
    {
        Uri = uri;
        LifetimeScope = lifetimeScope;
    }

    public Uri Uri { get; }

    public object LifetimeScope { get; }
}