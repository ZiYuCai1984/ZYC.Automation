using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.ModuleManager.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.ModuleManager;

[Register]
internal class NuGetModuleTabItem : TabItemInstanceBase<NuGetModuleManagerView>
{
    public NuGetModuleTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }


    public class Constants
    {
        public static string Icon => Base64IconResources.NuGet;

        public static string Host => LocalModuleTabItem.Constants.Host;

        public static string Path => "/nuget";

        public static string Title => "NuGet Modules";

        public static Uri Uri => UriTools.CreateAppUri(Host, Path);
    }
}