using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.About.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.About;

[Register]
internal class AboutTabItem : TabItemInstanceBase<AboutView>
{
    public AboutTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public static class Constants
    {
        public static string Icon => "InformationOutline";

        public static string Host => "about";

        public static string Title => "About";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}