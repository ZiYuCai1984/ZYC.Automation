namespace ZYC.Automation.Abstractions.Tab;

public interface ITabItemFactoryManager
{
    void RegisterFactory<T>() where T : ITabItemFactory;

    ITabItemFactory[] GetTabItemFactories();
}