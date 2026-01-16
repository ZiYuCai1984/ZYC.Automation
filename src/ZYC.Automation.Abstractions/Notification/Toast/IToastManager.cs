namespace ZYC.Automation.Abstractions.Notification.Toast;

public interface IToastManager
{
    void Prompt<T>() where T : IToast;

    void Prompt<T>(params object[] objects) where T : IToast;

    void PromptCopied();

    void PromptException(Exception exception);

    void PromptMessage(ToastMessage toastMessage);
}