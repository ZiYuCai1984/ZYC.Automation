using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WebView2.Commands;

[Register]
internal class OpenDevToolsWindowCommand : CommandBase, IViewSetter<WebViewHostBase>
{
    public WebViewHostBase? View { get; set; }

    public bool Disposing { get; set; }

    protected override void InternalExecute(object? parameter)
    {
        View!.OpenDevToolsWindow();
    }

    public override bool CanExecute(object? parameter)
    {
        return !View!.IsCoreWebView2Null();
    }
}