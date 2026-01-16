using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Event;
using ZYC.Automation.Abstractions.State;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Core.WindowTitle;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.WindowTitle.BuildIn;

[RegisterSingleInstance]
internal class FullScreenTitleItem : WindowTitleItem, INotifyPropertyChanged, IDisposable
{
    public FullScreenTitleItem(
        IEventAggregator eventAggregator,
        FullScreenCommand fullScreenCommand,
        DesktopWindowState desktopWindowState) : base(null!,
        null!)
    {
        FullScreenCommand = fullScreenCommand;
        DesktopWindowState = desktopWindowState;

        FullScreennCommandExecutedEvent =
            eventAggregator.Subscribe<FullScreennCommandExecutedEvent>(OnFullScreenCommandExecuted);
    }

    private IDisposable FullScreennCommandExecutedEvent { get; }

    private FullScreenCommand FullScreenCommand { get; }
    private DesktopWindowState DesktopWindowState { get; }

    public override string Icon
    {
        get
        {
            if (DesktopWindowState.WindowState == WindowState.Maximized)
            {
                return "ArrowCollapse";
            }

            return "ArrowExpandAll";
        }
    }

    public override ICommand Command => FullScreenCommand;

    public void Dispose()
    {
        FullScreennCommandExecutedEvent.Dispose();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnFullScreenCommandExecuted(FullScreennCommandExecutedEvent e)
    {
        OnPropertyChanged(nameof(Icon));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}