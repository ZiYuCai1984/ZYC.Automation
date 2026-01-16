using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Core.Page;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Tab.BuildIn;

[RegisterAs(typeof(ErrorView), typeof(IErrorView))]
internal partial class ErrorView : IErrorView
{
    public ErrorView(Exception exception, ILifetimeScope lifetimeScope, CopyAndNotifyCommand copyAndNotifyCommand)
    {
        Exception = exception;
        LifetimeScope = lifetimeScope;
        CopyAndNotifyCommand = copyAndNotifyCommand;

        InitializeComponent();
    }

    public Exception Exception { get; }

    private ILifetimeScope LifetimeScope { get; }

    public CopyAndNotifyCommand CopyAndNotifyCommand { get; }

    protected override void InternalOnLoaded()
    {
        Grid.Children.Add(LifetimeScope.Resolve<InnerErrorView>(
            new TypedParameter(typeof(Exception), Exception)));
    }
}