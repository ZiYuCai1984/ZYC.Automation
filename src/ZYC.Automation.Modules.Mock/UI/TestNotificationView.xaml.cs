using System.Windows;
using ZYC.Automation.Abstractions.Notification.Banner;
using ZYC.Automation.Abstractions.Notification.Toast;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Mock.UI;

[Register]
public partial class TestNotificationView
{
    public TestNotificationView(IToastManager toastManager, IBannerManager bannerManager)
    {
        ToastManager = toastManager;
        BannerManager = bannerManager;

        InitializeComponent();
    }

    private IToastManager ToastManager { get; }

    private IBannerManager BannerManager { get; }

    private void OnPromptRestartBtnClick(object sender, RoutedEventArgs e)
    {
        BannerManager.PromptRestart();
    }

    private void OnPromptToastBtnClick(object sender, RoutedEventArgs e)
    {
        ToastManager.PromptMessage(new ToastMessage("Hello World"));
    }
}