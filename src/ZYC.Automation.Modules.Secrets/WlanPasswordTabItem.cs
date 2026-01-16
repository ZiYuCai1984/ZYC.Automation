using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Secrets.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Secrets;

[Register]
internal class WlanPasswordTabItem : TabItemInstanceBase<WlanPasswordView>
{
    public WlanPasswordTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public class Constants
    {
        public static string Icon => "WifiLockOpen";

        // ReSharper disable once StringLiteralTypo
        public static string Host => "wlanpassword";

        public static string Title => "WlanPassword";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}