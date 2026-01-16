using Autofac;
using MahApps.Metro.IconPacks;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Chronosynchronicity.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

// ReSharper disable once CheckNamespace
namespace ZYC.Automation.Modules.Chronosynchronicity;

[Register]
internal class ChronosynchronicityTabItem : TabItemInstanceBase<ChronosynchronicityView>
{
    public ChronosynchronicityTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public class Constants
    {
        public static string Host => "chronosynchronicity";

        public static string Title => "Chronosynchronicity";

        public static string Icon => PackIconMaterialKind.Bug.ToString();

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}