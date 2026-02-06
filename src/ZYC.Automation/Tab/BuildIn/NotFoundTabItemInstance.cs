using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Tab;
using ZYC.Automation.Core.Tab;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Tab.BuildIn;

[RegisterAs(typeof(NotFoundTabItemInstance), typeof(INotFoundTabItemInstance))]
internal class NotFoundTabItemInstance : TabItemInstanceBase<NotFoundView>, INotFoundTabItemInstance
{
    public NotFoundTabItemInstance(ILifetimeScope lifetimeScope, TabReference tabReference) : base(lifetimeScope, tabReference)
    {
        
    }

    public static class Constants
    {
        public static string Title => "Page not found";

        public static string Icon => "BugOutline";
    }
}