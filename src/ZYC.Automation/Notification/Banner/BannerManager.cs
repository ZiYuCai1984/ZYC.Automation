using System.Diagnostics;
using Autofac;
using ZYC.Automation.Abstractions;
using ZYC.Automation.Abstractions.Notification;
using ZYC.Automation.Abstractions.Notification.Banner;
using ZYC.Automation.Notification.Banner.BuildIn;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Notification.Banner;

[RegisterSingleInstanceAs(typeof(IBannerManager))]
internal class BannerManager : IBannerManager
{
    private BottomBannerPopupHost? _bannerHost;

    public BannerManager(
        IAppContext appContext,
        ILifetimeScope lifetimeScope,
        IAppLogger<BannerManager> logger)
    {
        AppContext = appContext;
        LifetimeScope = lifetimeScope;
        Logger = logger;
    }

    private IAppContext AppContext { get; }

    private ILifetimeScope LifetimeScope { get; }

    private IAppLogger<BannerManager> Logger { get; }

    public void PromptRestart()
    {
        Prompt<PromptRestartBannerView>();
    }

    public void Prompt<T>() where T : IBanner
    {
        AppContext.InvokeOnUIThread(() =>
        {
            try
            {
                var banner = LifetimeScope.Resolve<T>();

                _bannerHost?.Dispose();
                _bannerHost = LifetimeScope.Resolve<BottomBannerPopupHost>(
                    new TypedParameter(typeof(INotification), banner));

                _bannerHost.Show();
            }
            catch (Exception e)
            {
                Debugger.Break();
                Logger.Error(e);
            }
        });
    }
}