using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.CLI.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI;

[Register]
internal class CLITabItem : TabItemInstanceBase<CLIView>
{
    public CLITabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public override bool Localization => false;

    public class Constants
    {
        public static string Host => "cli";

        public static string Title => "CLI";

        public static string Icon => "Console";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}