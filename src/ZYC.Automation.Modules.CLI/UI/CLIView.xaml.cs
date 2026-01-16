using System.Windows;
using ZYC.Automation.Modules.CLI.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.CLI.UI;

[Register]
internal partial class CLIView : IDisposable
{
    public CLIView(CLIConfig cliConfig)
    {
        CLIConfig = cliConfig;

        InitializeComponent();

        EasyTerminalControl.StartupCommandLine = CLIConfig.StartupCommandLine;
        EasyTerminalControl.Loaded += OnEasyTerminalControlLoaded;
    }

    private CLIConfig CLIConfig { get; }

    public void Dispose()
    {
        EasyTerminalControl.Loaded -= OnEasyTerminalControlLoaded;
    }

    private void OnEasyTerminalControlLoaded(object sender, RoutedEventArgs e)
    {
        EasyTerminalControl.Focus();
    }
}