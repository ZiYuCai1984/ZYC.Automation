using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using ZYC.Automation.Abstractions.Notification.Banner;
using ZYC.Automation.Core.Commands;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.UI.Banner;

[Register]
internal partial class PromptNewProductBanner : IBanner
{
    public PromptNewProductBanner(
        IUpdateManager updateManager,
        NavigateCommand navigateCommand)
    {
        UpdateManager = updateManager;

        var updateContext = UpdateManager.GetCurrentUpdateContext();

        Debug.Assert(updateContext.NewProduct != null);
        NewProduct = updateContext.NewProduct;

        NavigateCommand = new ActionCommand(_ =>
        {
            CloseBannerCommand.Execute(null);
            navigateCommand.Execute(UpdateModuleConstants.Uri);
        });

        InitializeComponent();
    }

    private IUpdateManager UpdateManager { get; }

    public ICommand NavigateCommand { get; }

    public NewProduct NewProduct { get; }

}