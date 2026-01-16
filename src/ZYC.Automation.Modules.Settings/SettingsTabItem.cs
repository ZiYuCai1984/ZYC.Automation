using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Settings.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Settings;

[Register]
internal class SettingsTabItem : TabItemInstanceBase<SettingsView>
{
    public SettingsTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public class Constants
    {
        public static string Icon => "ApplicationCogOutline";

        public static string Host => "settings";

        public static string Title => "ApplicationSettings";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}