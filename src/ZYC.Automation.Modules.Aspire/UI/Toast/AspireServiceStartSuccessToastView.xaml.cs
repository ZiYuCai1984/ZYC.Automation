using ZYC.Automation.Modules.Aspire.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.Aspire.UI.Toast;

[Register]
internal partial class AspireServiceStartSuccessToastView
{
    public AspireServiceStartSuccessToastView()
    {
        InitializeComponent();
    }


    public Uri TargetUri => AspireModuleContansts.Uri;
}