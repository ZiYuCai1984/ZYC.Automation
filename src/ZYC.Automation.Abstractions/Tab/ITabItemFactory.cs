namespace ZYC.Automation.Abstractions.Tab;

public interface ITabItemFactory
{
    bool IsSingle => true;

    Task<ITabItemInstance> CreateTabItemInstanceAsync(TabItemCreationContext context);

    Task<bool> CheckUriMatchedAsync(Uri uri);
}