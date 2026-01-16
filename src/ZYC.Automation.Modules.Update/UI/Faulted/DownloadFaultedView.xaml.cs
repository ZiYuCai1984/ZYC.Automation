using System.Diagnostics;
using System.Windows;
using Autofac;
using ZYC.Automation.Core.Page;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.UI.Faulted;

[Register]
internal partial class DownloadFaultedView
{
    public DownloadFaultedView(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;
        InitializeComponent();
    }

    private ILifetimeScope LifetimeScope { get; }

    private void OnDownloadFaultedViewLoaded(object sender, RoutedEventArgs e)
    {
        var ex = (Exception)Tag;
        Debug.Assert(ex != null);

        Border.Child = LifetimeScope.Resolve<InnerErrorView>(
            new TypedParameter(typeof(Exception), ex));
    }
}