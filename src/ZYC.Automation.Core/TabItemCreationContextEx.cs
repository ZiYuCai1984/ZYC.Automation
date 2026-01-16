using Autofac;
using Autofac.Core;
using ZYC.Automation.Abstractions.Tab;

namespace ZYC.Automation.Core;

public static class TabItemCreationContextEx
{
    public static T Resolve<T>(this TabItemCreationContext context) where T : notnull
    {
        return context.GetLifetimeScope().Resolve<T>();
    }

    public static T Resolve<T>(this TabItemCreationContext context, params Parameter[] parameters) where T : notnull
    {
        return context.GetLifetimeScope().Resolve<T>(parameters);
    }

    public static ILifetimeScope GetLifetimeScope(this TabItemCreationContext context)
    {
        return (ILifetimeScope)context.LifetimeScope;
    }
}