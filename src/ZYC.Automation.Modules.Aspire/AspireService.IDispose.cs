namespace ZYC.Automation.Modules.Aspire;

internal partial class AspireService : IDisposable
{
    public bool IsDisposed { get; set; }

    /// <summary>
    ///     !WARNING This Dispose may be a time-consuming operation.
    /// </summary>
    public void Dispose()
    {
        lock (this)
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            Gate.Dispose();
            AspireDashboardReadyEvent.Dispose();
            DistributedApplication.Dispose();
        }
    }
}