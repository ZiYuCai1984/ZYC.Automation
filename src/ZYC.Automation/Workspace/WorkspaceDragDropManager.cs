using System.Windows;
using Autofac;
using ZYC.Automation.Abstractions.Workspace;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Workspace;

[RegisterSingleInstanceAs(
    typeof(IWorkspaceDragDropManager), typeof(WorkspaceDragDropManager))]
internal class WorkspaceDragDropManager : IWorkspaceDragDropManager
{
    public WorkspaceDragDropManager(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
    }

    private ILifetimeScope LifetimeScope { get; }

    private IWorkspaceDragDropProvider[] WorkspaceDragDropProviders { get; set; } = [];

    public void Register<T>() where T : IWorkspaceDragDropProvider
    {
        var newProvider = LifetimeScope.Resolve<T>();
        var workspaceDragDropManagers = new List<IWorkspaceDragDropProvider>(WorkspaceDragDropProviders)
            { newProvider };

        WorkspaceDragDropProviders = workspaceDragDropManagers.OrderBy(t => t.Priority).ToArray();
    }

    public void OnDragEnter(WorkspaceNode node, DragEventArgs dragEventArgs)
    {
        foreach (var p in WorkspaceDragDropProviders)
        {
            if (p.HandleDragEnter(node, dragEventArgs))
            {
                break;
            }
        }
    }

    public void OnDrop(WorkspaceNode node, DragEventArgs dragEventArgs)
    {
        foreach (var p in WorkspaceDragDropProviders)
        {
            if (p.HandleDrop(node, dragEventArgs))
            {
                break;
            }
        }
    }

    public void OnDragOver(WorkspaceNode node, DragEventArgs dragEventArgs)
    {
        foreach (var p in WorkspaceDragDropProviders)
        {
            if (p.HandleDragOver(node, dragEventArgs))
            {
                break;
            }
        }
    }
}