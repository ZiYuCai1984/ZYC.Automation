using System.Collections.Concurrent;
using System.Reflection;
using Autofac;
using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit;

namespace ZYC.Automation.Core.Tab;

public abstract class TabItemInstanceBase<T> : TabItemInstanceBase where T : notnull
{
    protected TabItemInstanceBase(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public override object View => _view ??= LifetimeScope.Resolve<T>();
}

public abstract class TabItemInstanceBase : ITabItemInstance
{
    // ReSharper disable once InconsistentNaming
    protected object? _view;

    protected TabItemInstanceBase(ILifetimeScope lifetimeScope)
    {
        LifetimeScope = lifetimeScope;

        TabReference = new TabReference(GetConstant<Uri>(nameof(Uri)));
    }

    protected ILifetimeScope LifetimeScope { get; }

    private ConcurrentDictionary<(Type, string), object?> ConstantsCache { get; } = new();

    public TabReference TabReference { get; }

    public Guid Id => TabReference.Id;

    public virtual string Host => GetConstant<string>(nameof(Host));

    public virtual string Title => GetConstant<string>(nameof(Title));

    public virtual string Icon => GetConstant<string>(nameof(Icon));

    public Uri Uri
    {
        get => TabReference.Uri;
        set => TabReference.Uri = value;
    }

    public abstract object View { get; }

    public virtual bool Localization => true;


    public virtual Task LoadAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///     OnClosing
    ///     true : Allow close
    ///     false : Prevent close
    /// </summary>
    public virtual bool OnClosing()
    {
        return true;
    }

    public virtual void Dispose()
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        View?.TryDispose();
    }
    //TODO Design failure !!
    protected T GetConstant<T>(string propertyName)
    {
        var key = (GetType(), propertyName);

        return (T)ConstantsCache.GetOrAdd(key, k =>
        {
            var (type, prop) = k;

            var nestedTypeName = "Constants";

            var constantsType = type.GetNestedType(nestedTypeName, BindingFlags.Public | BindingFlags.NonPublic);
            if (constantsType == null)
            {
                throw new InvalidOperationException(
                    $"<{nestedTypeName}> must be defined in <{type.Name}>");
            }

            var property = constantsType.GetProperty(prop, BindingFlags.Public | BindingFlags.Static);
            if (property == null)
            {
                throw new InvalidOperationException(
                    $"<{prop}> static property missing in <{nestedTypeName}>");
            }

            return property.GetValue(null);
        })!;
    }
}