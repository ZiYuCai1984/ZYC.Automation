using ZYC.Automation.Abstractions.Tab;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Tab.BuildIn;

[RegisterAs(typeof(ITabItemHeaderContextMenuItemView))]
internal partial class UnlockTabItemHeaderContextMenuItem : ITabItemHeaderContextMenuItemView
{
    protected override void InternalOnMenuItemBaseLoaded()
    {
        InitializeComponent();
    }
}