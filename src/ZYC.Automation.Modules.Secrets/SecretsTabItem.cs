using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Secrets.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Secrets;

[Register]
internal class SecretsTabItem : TabItemInstanceBase<SecretsManagerView>
{
    public SecretsTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public class Constants
    {
        public static string Icon => "EyeOutline";

        public static string Host => "secrets";

        public static string Title => "Secrets";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}