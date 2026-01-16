using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.Update.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update;

[Register]
internal class UpdateTabItem : TabItemInstanceBase<UpdateView>
{
    public UpdateTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }


    public class Constants
    {
        public static string Icon => "Update";

        public static string Host => "update";

        public static string Title => "Update";

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}