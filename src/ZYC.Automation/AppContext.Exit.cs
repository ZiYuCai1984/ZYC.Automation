using System.Windows;
using System.Windows.Threading;
using ZYC.Automation.Abstractions;

namespace ZYC.Automation;

internal partial class AppContext
{
    public void ExitProcess()
    {
        if (Exiting != null)
        {
            var cancelEventArgsEx = new CancelEventArgsEx();
            Exiting.Invoke(this, cancelEventArgsEx);
            if (cancelEventArgsEx.Cancel)
            {
                return;
            }
        }

        Shutdown(0);
    }


    public new event EventHandler? Exit;

    public event EventHandler<CancelEventArgsEx>? Exiting;

    void IAppContext.FocusExitProcess()
    {
        FocusExitProcess(Logger);
    }

    public static void FocusExitProcess(IAppLogger<AppContext>? logger = null)
    {
        logger?.Warn("Focus exit !!");
        Environment.Exit(0);
    }


    private void OnAppDomainExceptionUnhandled(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception ex)
        {
            Logger.Error(ex);
        }
        else
        {
            Logger.Error(
                $"An unhandled exception has been thrown in the current AppDomain:\r\n{e.ExceptionObject}");
        }


        //!WARNING Do not save state and configuration when the program crashes
    }

    private void OnTaskExceptionUnhandled(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        Logger.Error(e.Exception);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        Exit?.Invoke(this, e);

        SaveAllConfig();
        SaveAllState();

        foreach (var module in Modules)
        {
            await module.UnloadAsync(LifetimeScope);
        }
    }


    private void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = AppConfig.HandleGlobalException;
        Logger.Error(e.Exception);
    }
}