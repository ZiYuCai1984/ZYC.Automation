using EasyWindowsTerminalControl;
using Microsoft.Extensions.Logging;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Modules.CLI.Abstractions;
using ZYC.CoreToolkit;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI.UI;

[Register]
internal partial class CLIView : IDisposable
{
    public CLIView(
        CLIConfig cliConfig,
        CLIUriOptions cliUriOptions, ILogger<CLIView> logger)
    {
        CLIConfig = cliConfig;
        CLIUriOptions = cliUriOptions;
        Logger = logger;


        InitializeComponent();

        if (string.IsNullOrWhiteSpace(cliUriOptions.StartupCommandLineOverride))
        {
            EasyTerminalControl.StartupCommandLine = CLIConfig.StartupCommandLine;
        }


        ConPTYTerm.TermReady += OnConPTYTermTermReady;
    }

    private TermPTY ConPTYTerm => EasyTerminalControl.ConPTYTerm;

    private CLIConfig CLIConfig { get; }

    private CLIUriOptions CLIUriOptions { get; }

    private ILogger<CLIView> Logger { get; }

    private bool IsDisposed { get; set; }

    public void Dispose()
    {
        if (IsDisposed)
        {
            DebuggerTools.Break();
        }

        IsDisposed = true;

        var term = EasyTerminalControl.DisconnectConPTYTerm();
        term.TermReady -= OnConPTYTermTermReady;

        term.CloseStdinToApp();
        term.StopExternalTermOnly();
        term.Process.Kill();
    }

    private async void OnConPTYTermTermReady(object? sender, EventArgs e)
    {
        try
        {
            //!WARNING To prevent strange reentrancy issues on the first boot
            await Task.Delay(500);

            await Dispatcher.InvokeAsync(async () =>
            {
                if (!string.IsNullOrWhiteSpace(CLIUriOptions.TypeText))
                {
                    ConPTYTerm.WriteToTerm(CLIUriOptions.TypeText);

                    if (CLIUriOptions.TypeOnly)
                    {
                        return;
                    }

                    await ConPTYTerm.ExecuteAndWaitAsync(
                        CLIUriOptions.TypeText);
                }

                if (CLIUriOptions.ExecCommands is { Count: > 0 })
                {
                    foreach (var command in CLIUriOptions.ExecCommands)
                    {
                        if (string.IsNullOrWhiteSpace(command))
                        {
                            continue;
                        }

                        await ConPTYTerm.ExecuteAndWaitAsync(
                            command);
                    }
                }
            });
        }
        catch (Exception ex)
        {
            Logger.Error(ex);
        }
    }
}