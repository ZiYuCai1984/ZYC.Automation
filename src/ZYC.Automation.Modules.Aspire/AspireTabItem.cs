using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Aspire.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire;

[Register]
internal class AspireTabItem : TabItemInstanceBase<AspireView>
{
    public AspireTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public override bool Localization => false;

    public class Constants
    {
        public static string Host => "aspire";

        public static string Title => "Aspire";

        public static string Icon => Base64IconResources.Aspire;

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}