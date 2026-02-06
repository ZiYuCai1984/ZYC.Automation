using System.Diagnostics;

namespace ZYC.Automation.WebView2;

public partial class WebViewHostBase
{
    protected bool Disposing { get; private set; }

    public virtual void Dispose()
    {
        if (Disposing)
        {
            Debugger.Break();
            return;
        }

        Disposing = true;

        InternalDispose();

        CompositeDisposable.Dispose();

        WebView2.Dispose();
        InnerHttpClient.Dispose();
    }

    protected virtual void InternalDispose()
    {
    }
}