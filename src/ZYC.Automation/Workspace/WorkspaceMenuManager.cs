using Autofac;
using MahApps.Metro.IconPacks;
using ZYC.Automation.Abstractions.Workspace;
using ZYC.Automation.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Workspace;

[RegisterSingleInstanceAs(typeof(IWorkspaceMenuManager))]
internal class WorkspaceMenuManager : IWorkspaceMenuManager
{
    public WorkspaceMenuManager(
        ILifetimeScope lifetimeScope,
        ToggleOrientationCommand toggleOrientationCommand,
        SwapCommand swapCommand,
        ResetCommand resetCommand,
        SplitVerticalCommand splitVerticalCommand,
        MergeCommand mergeCommand,
        SetFocusedWorkspaceCommand focusedWorkspaceCommand,
        SplitHorizontalCommand splitHorizontalCommand)
    {
        LifetimeScope = lifetimeScope;
        WorkspaceMenuItems.Add(
            new WorkspaceMenuItem(
                "Reset",
                resetCommand,
                PackIconMaterialKind.Reload.ToString()));

        WorkspaceMenuItems.Add(
            new WorkspaceMenuItem(
                "ToggleOrientation",
                toggleOrientationCommand,
                PackIconMaterialKind.OrbitVariant.ToString()));

        WorkspaceMenuItems.Add(
            new WorkspaceMenuItem(
                "Swap",
                swapCommand,
                PackIconMaterialKind.SwapHorizontal.ToString()));

        WorkspaceMenuItems.Add(
            new WorkspaceMenuItem(
                "Merge",
                mergeCommand,
                PackIconMaterialKind.FlipToFront.ToString()));

        WorkspaceMenuItems.Add(
            new WorkspaceMenuItem(
                "SplitVertical",
                splitVerticalCommand,
                PackIconMaterialKind.FlipVertical.ToString()));

        WorkspaceMenuItems.Add(
            new WorkspaceMenuItem(
                "SplitHorizontal",
                splitHorizontalCommand,
                PackIconMaterialKind.FlipHorizontal.ToString()));

        WorkspaceMenuItems.Add(
            new WorkspaceMenuItem(
                "Focus",
                focusedWorkspaceCommand,
                PackIconMaterialKind.ImageFilterCenterFocus.ToString()));
    }

    private ILifetimeScope LifetimeScope { get; }

    private List<IWorkspaceMenuItem> WorkspaceMenuItems { get; } = new();

    public void RegisterItem(IWorkspaceMenuItem item)
    {
        WorkspaceMenuItems.Add(item);
    }

    public void RegisterItem<T>() where T : IWorkspaceMenuItem
    {
        var item = LifetimeScope.Resolve<T>();
        WorkspaceMenuItems.Add(item);
    }

    public IWorkspaceMenuItem[] GetItems()
    {
        return WorkspaceMenuItems
            .ToArray();
    }
}