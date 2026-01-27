using System.Windows;
using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Mock.UI;

[Register]
public partial class TestCLIView
{
    public TestCLIView(ITabManager tabManager)
    {
        TabManager = tabManager;
        InitializeComponent();
    }

    private ITabManager TabManager { get; }

    private async void OnNavigate50TimesButtonClick(object sender, RoutedEventArgs e)
    {
        try
        {
            for (var i = 0; i < 50; ++i)
            {
                await TabManager.NavigateAsync("zyc://cli");
                await Task.Delay(50);
            }
        }
        catch
        {
            //ignore
        }
    }
}