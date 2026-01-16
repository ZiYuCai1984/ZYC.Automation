using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Tab.BuildIn;

/// <summary>
///     !WARNING To keep the structure simple and less prone to errors, it does not inherit from TabItemInstanceBase
/// </summary>
[Register]
internal class ErrorTabItemInstance : ITabItemInstance
{
    private object? _view;

    public ErrorTabItemInstance(Uri? uri, Exception exception, ILifetimeScope lifetimeScope)
    {
        Uri = uri!;
        Exception = exception;
        LifetimeScope = lifetimeScope;

        TabReference = new TabReference(Uri);
    }

    private Exception Exception { get; }
    private ILifetimeScope LifetimeScope { get; }

    public object View => _view ??= LifetimeScope.Resolve<ErrorView>(
        new TypedParameter(typeof(Exception), Exception));

    public void Dispose()
    {
        //ignore
    }

    public TabReference TabReference { get; }

    public Guid Id => TabReference.Id;

    public Uri Uri { get; }

    public string Scheme => Uri.Scheme;

    public string Host => UriTools.TempHost;

    public string Title => "Error";

    public string Icon => "BugOutline";

    public bool Localization => true;

    public Task LoadAsync()
    {
        return Task.CompletedTask;
    }

    public bool OnClosing()
    {
        return true;
    }
}