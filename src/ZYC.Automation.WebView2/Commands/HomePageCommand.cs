using ZYC.Automation.Abstractions;
using ZYC.Automation.Core.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WebView2.Commands;

[Register]
internal class HomePageCommand : AsyncCommandBase, IViewSetter<WebViewHostBase>
{
    public WebViewHostBase? View { get; set; }

    public bool Disposing { get; set; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        if (View!.HomePageUri != null)
        {
            await View.NavigateAsync(View.HomePageUri);
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return View!.HomePageUri != null;
    }
}