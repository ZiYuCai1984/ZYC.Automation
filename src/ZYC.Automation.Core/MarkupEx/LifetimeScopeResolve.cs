using System.Windows.Markup;
using Autofac;
using Autofac.Core;
using ZYC.Automation.Core.Resources;

namespace ZYC.Automation.Core.MarkupEx;

public sealed class LifetimeScopeResolve : MarkupExtension
{
#pragma warning disable CS8618
    public LifetimeScopeResolve()

    {
    }

    public LifetimeScopeResolve(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public List<AutofacCtorParam> Parameters { get; } = new();

    public object? Parameter { get; set; }
    public Type? ParameterType { get; set; }
    public string? ParameterName { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var lifetimeScope = LifetimeScopeResource.GetRootLifetimeScopeFromMainWindowDataContext();

        var ps = new List<Parameter>();

        if (ParameterName is { Length: > 0 })
        {
            ps.Add(new NamedParameter(ParameterName, Parameter));
        }
        else if (Parameter is not null || ParameterType is not null)
        {
            var t = ParameterType ?? Parameter!.GetType();
            ps.Add(new TypedParameter(t, Parameter));
        }

        if (Parameters.Count > 0)
        {
            ps.AddRange(Parameters.Select(p => p.ToAutofacParameter()));
        }

        return ps.Count == 0
            ? lifetimeScope.Resolve(Type)
            : lifetimeScope.Resolve(Type, ps);
    }
}

public sealed class AutofacCtorParam
{
    public string? Name { get; set; }

    public Type? Type { get; set; }

    public object? Value { get; set; }

    internal Parameter ToAutofacParameter()
    {
        if (!string.IsNullOrWhiteSpace(Name))
        {
            return new NamedParameter(Name!, Value);
        }

        var t = Type ?? Value?.GetType();
        if (t is null)
        {
            throw new InvalidOperationException("When Value is null, you must set Type.");
        }

        return new TypedParameter(t, Value);
    }
}