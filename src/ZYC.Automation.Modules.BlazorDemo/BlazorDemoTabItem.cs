using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Tab;
using ZYC.Automation.Modules.BlazorDemo.UI;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.BlazorDemo;

[Register]
internal class BlazorDemoTabItem : TabItemInstanceBase<BlazorDemoView>
{
    public BlazorDemoTabItem(ILifetimeScope lifetimeScope) : base(lifetimeScope)
    {
    }

    public override bool Localization => false;

    public class Constants
    {
        // ReSharper disable once StringLiteralTypo
        public static string Host => "blazordemo";

        public static string Title => "BlazorDemo";

        public static string Icon => Base64IconResources.Blazor;

        public static Uri Uri => UriTools.CreateAppUri(Host);
    }
}