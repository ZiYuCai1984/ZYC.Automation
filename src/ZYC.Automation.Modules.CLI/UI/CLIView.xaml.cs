using System.Windows;
using Microsoft.Extensions.Logging;
using ZYC.Automation.Modules.CLI.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI.UI;

[Register]
internal partial class CLIView : IDisposable
{
    public CLIView(CLIConfig cliConfig,ILogger<CLIView> logger)
    {
        CLIConfig = cliConfig;
        Logger = logger;

        InitializeComponent();

        EasyTerminalControl.StartupCommandLine = CLIConfig.StartupCommandLine;
        EasyTerminalControl.Loaded += OnEasyTerminalControlLoaded;
    }

    private CLIConfig CLIConfig { get; }
    private ILogger<CLIView> Logger { get; }

    public void Dispose()
    {
        EasyTerminalControl.Loaded -= OnEasyTerminalControlLoaded;
        EasyTerminalControl.DisconnectConPTYTerm();
    }

    private void OnEasyTerminalControlLoaded(object sender, RoutedEventArgs e)
    {
        EasyTerminalControl.Focus();
    }
}