using ZYC.Automation.Abstractions.State;
using ZYC.Automation.Abstractions.Workspace;

namespace ZYC.Automation.Abstractions.Tab;

public interface ITabManager
{
    NavigationState GetNavigationState(Guid workspaceId);

    ITabItemInstance? GetFocusedTabItemInstance(Guid workspaceId);

    void SetFocusedTabItemInstance(Guid workspaceId, ITabItemInstance? instance);

    ITabItemInstance[] GetTabItemInstances(Guid workspaceId);

    WorkspaceNode GetTabItemInstanceWorkspace(ITabItemInstance instance);

    Task NavigateAsync(Uri uri);

    Task NavigateAsync(string uri)
    {
        return NavigateAsync(new Uri(uri));
    }

    Task NavigateAsync(Guid workspaceId, Uri uri);

    Task NavigateAsync(Guid workspaceId, string uri)
    {
        return NavigateAsync(workspaceId, new Uri(uri));
    }

    void MoveTabItemInstance(ITabItemInstance instance, Guid from, Guid to);

    void MoveAllTabItemInstances(Guid from, Guid to);

    /// <summary>
    ///     Change the url inside the tab
    /// </summary>
    Task TabInternalNavigatingAsync(Uri oriUri, Uri newUri);

    /// <summary>
    ///     Focus to an existing tab
    /// </summary>
    Task FocusAsync(Uri uri);

    Task ReloadAsync(Uri uri);

    Task CloseAsync(ITabItemInstance instance);

    Task CloseAllAsync();

    Task RestoreStateAsync();
}