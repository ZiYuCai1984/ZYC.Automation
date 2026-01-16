using System.Diagnostics;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.Automation.Modules.Update.Commands;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.UI.Banner;

[Register]
internal partial class PromptPendingRestartBanner
{
    public PromptPendingRestartBanner(
        ApplyAndRestartCommand applyAndRestartCommand,
        IUpdateManager updateManager)
    {
        ApplyAndRestartCommand = applyAndRestartCommand;

        var product = updateManager.GetCurrentUpdateContext().NewProduct;
        Debug.Assert(product != null);

        NewProduct = product;


        InitializeComponent();
    }

    public ApplyAndRestartCommand ApplyAndRestartCommand { get; }

    public NewProduct NewProduct { get; }
}