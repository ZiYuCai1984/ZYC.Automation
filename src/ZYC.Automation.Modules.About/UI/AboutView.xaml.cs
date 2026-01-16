using System.Windows.Data;
using ZYC.Automation.Abstractions;
using ZYC.CoreToolkit.Extensions.Autofac.Attributes;

namespace ZYC.Automation.Modules.About.UI;

[Register]
internal partial class AboutView
{
    public AboutView(IAppContext appContext, IProduct product)
    {
        CurrentProduct = product;
        IsSelfAlternate = appContext.IsSelfAlternate();

        StackPanel.SetBinding(DataContextProperty, new Binding
        {
            Source = this
        });
    }

    public IProduct CurrentProduct { get; }

    public bool IsSelfAlternate { get; }
}