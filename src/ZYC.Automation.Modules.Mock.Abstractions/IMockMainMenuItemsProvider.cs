using ZYC.Automation.Abstractions.MainMenu;

namespace ZYC.Automation.Modules.Mock.Abstractions;

/// <summary>
///     Provides main menu items for the mock module.
/// </summary>
public interface IMockMainMenuItemsProvider : IMainMenuItemsProvider
{
    /// <summary>
    ///     Registers a mock tab item as a submenu entry.
    /// </summary>
    /// <param name="mockTabItemInfo">The mock tab item to register.</param>
    void RegisterSubItem(MockTabItemInfo mockTabItemInfo);
}