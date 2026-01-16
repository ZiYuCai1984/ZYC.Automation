using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.ModuleManager.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[Register]
internal class LocalModuleTabItem : TabItemInstanceBase<LocalModuleManagerView>
{
    public LocalModuleTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public class Constants
    {
        public static string Icon => "PackageVariantClosed";

        public static string Host => "modules";

        public static string Path => "/local";

        public static string Title => "Local Modules";

        public static Uri Uri => UriTools.CreateAppUri(Host, Path);
    }
}