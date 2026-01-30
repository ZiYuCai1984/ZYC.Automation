namespace ZYC.Automation.Abstractions.Tab;

public interface ITabItemFactory
{
    bool IsSingle => true;

    int Priority => 0;

    Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context);

    Task<bool> CheckUriMatchedAsync(Uri uri);
}