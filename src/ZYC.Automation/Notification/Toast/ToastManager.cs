using System.Diagnostics;
using Autofac;
using Autofac.Core;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Notification.Toast;
using ZYC.Automation.Notification.Toast.BuildIn;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Notification.Toast;

[RegisterSingleInstanceAs(typeof(IToastManager))]
internal class ToastManager : IToastManager
{
    private ToastStackPopupHost? _toastStackPopupHost;

    public ToastManager(
        IAppContext appContext,
        ILifetimeScope lifetimeScope,
        IAppLogger<ToastManager> logger)
    {
        AppContext = appContext;
        LifetimeScope = lifetimeScope;
        Logger = logger;
    }

    private ToastStackPopupHost ToastStackPopupHost =>
        _toastStackPopupHost ??= LifetimeScope.Resolve<ToastStackPopupHost>();

    private IAppContext AppContext { get; }

    private ILifetimeScope LifetimeScope { get; }

    private IAppLogger<ToastManager> Logger { get; }

    public void Prompt<T>() where T : IToast
    {
        AppContext.InvokeOnUIThread(() =>
        {
            try
            {
                var toast = LifetimeScope.Resolve<T>();
                ImplPrompt(toast);
            }
            catch (Exception e)
            {
                Debugger.Break();
                Logger.Error(e);
            }
        });
    }

    public void Prompt<T>(params object[] objects) where T : IToast
    {
        var parameters = new List<Parameter>();
        foreach (var obj in objects)
        {
            parameters.Add(new TypedParameter(obj.GetType(), obj));
        }

        AppContext.InvokeOnUIThread(() =>
        {
            try
            {
                var toast = LifetimeScope.Resolve<T>(parameters);
                ImplPrompt(toast);
            }
            catch (Exception e)
            {
                Debugger.Break();
                Logger.Error(e);
            }
        });
    }


    public void PromptCopied()
    {
        Prompt<PromptCopiedToastView>();
    }

    public void PromptException(Exception exception)
    {
        AppContext.InvokeOnUIThread(() =>
        {
            try
            {
                var toast = LifetimeScope.Resolve<PromptExceptionToastView>(
                    new TypedParameter(typeof(Exception), exception));
                ImplPrompt(toast);
            }
            catch (Exception e)
            {
                Debugger.Break();
                Logger.Error(e);
            }
        });
    }

    public void PromptMessage(ToastMessage toastMessage)
    {
        AppContext.InvokeOnUIThread(() =>
        {
            try
            {
                var toast = LifetimeScope.Resolve<PromptMessageToastView>(
                    new TypedParameter(typeof(ToastMessage), toastMessage));
                ImplPrompt(toast);
            }
            catch (Exception e)
            {
                Debugger.Break();
                Logger.Error(e);
            }
        });
    }

    private void ImplPrompt<T>(T view) where T : IToast
    {
        ToastStackPopupHost.Add(view);
    }
}