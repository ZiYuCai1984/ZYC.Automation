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
internal class SetTopmostTitleItem : WindowTitleItem, INotifyPropertyChanged, IDisposable
{
    private readonly SetTopmostCommand _command;

    public SetTopmostTitleItem(
        IEventAggregator eventAggregator,
        DesktopWindowState desktopWindowState,
        SetTopmostCommand command) : base(null!, null!)
    {
        _command = command;
        DesktopWindowState = desktopWindowState;

        SetTopmostCommandExecutedEvent =
            eventAggregator.Subscribe<SetTopmostCommandExecutedEvent>(OnSetTopmostCommandExecuted);
    }

    private IDisposable SetTopmostCommandExecutedEvent { get; }

    private DesktopWindowState DesktopWindowState { get; }

    public override string Icon
    {
        get
        {
            if (DesktopWindowState.Topmost)
            {
                return "PinOffOutline";
            }

            return "PinOutline";
        }
    }

    public override ICommand Command => _command;

    public void Dispose()
    {
        SetTopmostCommandExecutedEvent.Dispose();
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnSetTopmostCommandExecuted(SetTopmostCommandExecutedEvent obj)
    {
        OnPropertyChanged(nameof(Icon));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}