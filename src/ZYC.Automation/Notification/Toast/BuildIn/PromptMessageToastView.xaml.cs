using ZYC.Automation.Abstractions.Notification.Toast;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Notification.Toast.BuildIn;

[Register]
internal partial class PromptMessageToastView
{
    public PromptMessageToastView(ToastMessage toastMessage)
    {
        ToastMessage = toastMessage;

        InitializeComponent();
    }

    public ToastMessage ToastMessage { get; }
}