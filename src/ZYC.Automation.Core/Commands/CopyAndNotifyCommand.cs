using System.Windows;
using ZYC.Automation.Abstractions.Notification.Toast;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Core.Commands;

[RegisterSingleInstance]
public class CopyAndNotifyCommand : AsyncCommandBase
{
    public CopyAndNotifyCommand(IToastManager toastManager)
    {
        ToastManager = toastManager;
    }

    private IToastManager ToastManager { get; }

    protected override async Task InternalExecuteAsync(object? parameter)
    {
        await Task.CompletedTask;

        var value = parameter?.ToString();

        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        Clipboard.SetText(value);
        ToastManager.PromptCopied();
    }

    public override bool CanExecute(object? parameter)
    {
        var value = parameter?.ToString();
        return !string.IsNullOrEmpty(value);
    }
}