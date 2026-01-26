namespace ZYC.Automation.Modules.Aspire;

internal partial class AspireServiceManager
{
    public void Dispose()
    {
        if (AspireService != null && !AspireService.IsDisposed)
        {
            AspireService.Dispose();
        }

        Gate.Dispose();
        CompositeDisposable.Dispose();
    }
}