using ZYC.Automation.Abstractions;
using ZYC.Automation.Modules.Update.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Update.UI.Toast;

[Register]
internal partial class PromptNewProductToastView
{
    public PromptNewProductToastView(NewProduct newProduct)
    {
        NewProduct = newProduct;

        InitializeComponent();
    }

    public NewProduct NewProduct { get; }

    public Uri TargetUri => UriTools.CreateAppUri("update");
}