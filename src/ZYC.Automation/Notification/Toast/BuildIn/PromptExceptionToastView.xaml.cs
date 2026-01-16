using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Notification.Toast.BuildIn;

[Register]
internal partial class PromptExceptionToastView
{
    public PromptExceptionToastView(Exception exception)
    {
        Exception = exception;

        InitializeComponent();
    }

    public Exception Exception { get; }
}